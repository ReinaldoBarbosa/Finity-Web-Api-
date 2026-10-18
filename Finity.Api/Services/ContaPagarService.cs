using Finity.Api.Data;
using Finity.Api.Dtos.ContasPagar;
using Finity.Api.Interfaces;
using Finity.Api.Models.Financeiro;
using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Services;

public class ContaPagarService : IContaPagarService
{
    private readonly AppDbContext _context;

    public ContaPagarService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ContaPagarRespostaDto>> ListarAsync()
    {
        return await _context.ContasPagar
            .AsNoTracking()
            .OrderBy(c => c.DataVencimento)
            .ThenBy(c => c.Id)
            .Select(c => new ContaPagarRespostaDto
            {
                Id = c.Id,
                IdEmpresa = c.IdEmpresa,
                IdCategoria = c.IdCategoria,
                TipoCategoria = c.TipoCategoria,
                IdCentroCusto = c.IdCentroCusto,
                Descricao = c.Descricao,
                ValorOriginal = c.ValorOriginal,
                DataEmissao = c.DataEmissao,
                DataVencimento = c.DataVencimento,
                DataCadastro = c.DataCadastro,
                Cancelada = c.Cancelada
            })
            .ToListAsync();
    }

    public async Task<ContaPagarRespostaDto?> BuscarPorIdAsync(int id)
    {
        var conta = await _context.ContasPagar
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return conta is null ? null : Mapear(conta);
    }

    public async Task<ContaPagarRespostaDto> CriarAsync(
        CriarContaPagarDto dto)
    {
        ValidarDados(dto.Descricao, dto.TipoCategoria,
            dto.DataEmissao, dto.DataVencimento, dto.ValorOriginal);

        var tipo = NormalizarTipo(dto.TipoCategoria);
        await ValidarRelacionamentosAsync(
            dto.IdEmpresa, dto.IdCategoria, tipo, dto.IdCentroCusto);

        var conta = new ContaPagar
        {
            IdEmpresa = dto.IdEmpresa,
            IdCategoria = dto.IdCategoria,
            TipoCategoria = tipo,
            IdCentroCusto = dto.IdCentroCusto,
            Descricao = dto.Descricao.Trim(),
            ValorOriginal = dto.ValorOriginal,
            DataEmissao = dto.DataEmissao,
            DataVencimento = dto.DataVencimento,
            DataCadastro = DateTime.Now,
            Cancelada = false
        };

        _context.ContasPagar.Add(conta);
        await _context.SaveChangesAsync();

        return Mapear(conta);
    }

    public async Task<ContaPagarRespostaDto?> AtualizarAsync(
        int id, AtualizarContaPagarDto dto)
    {
        var conta = await _context.ContasPagar
            .FirstOrDefaultAsync(c => c.Id == id);

        if (conta is null)
            return null;

        if (conta.Cancelada)
            throw new InvalidOperationException(
                "Não é possível atualizar uma conta cancelada.");

        ValidarDados(dto.Descricao, dto.TipoCategoria,
            dto.DataEmissao, dto.DataVencimento, dto.ValorOriginal);

        var tipo = NormalizarTipo(dto.TipoCategoria);

        await ValidarRelacionamentosAsync(
            conta.IdEmpresa, dto.IdCategoria, tipo, dto.IdCentroCusto);

        conta.IdCategoria = dto.IdCategoria;
        conta.TipoCategoria = tipo;
        conta.IdCentroCusto = dto.IdCentroCusto;
        conta.Descricao = dto.Descricao.Trim();
        conta.ValorOriginal = dto.ValorOriginal;
        conta.DataEmissao = dto.DataEmissao;
        conta.DataVencimento = dto.DataVencimento;

        await _context.SaveChangesAsync();

        return Mapear(conta);
    }

    private async Task ValidarRelacionamentosAsync(
        int idEmpresa, int idCategoria, string tipo, int? idCentroCusto)
    {
        var empresa = await _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == idEmpresa);

        if (empresa is null)
            throw new ArgumentException("A empresa informada não existe.");

        if (!empresa.Ativo)
            throw new InvalidOperationException(
                "A empresa informada está inativa.");

        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c =>
                c.IdEmpresa == idEmpresa &&
                c.Id == idCategoria &&
                c.Tipo == tipo);

        if (categoria is null)
            throw new ArgumentException(
                "A categoria não existe para esta empresa ou não corresponde ao tipo da conta.");

        if (!categoria.Ativo)
            throw new InvalidOperationException(
                "A categoria informada está inativa.");

        if (idCentroCusto.HasValue)
        {
            var centro = await _context.CentrosCusto
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.IdEmpresa == idEmpresa &&
                    c.Id == idCentroCusto.Value);

            if (centro is null)
                throw new ArgumentException(
                    "O centro de custo não existe para esta empresa.");

            if (!centro.Ativo)
                throw new InvalidOperationException(
                    "O centro de custo informado está inativo.");
        }
    }

    private static void ValidarDados(
        string descricao, string tipoCategoria,
        DateOnly dataEmissao, DateOnly dataVencimento, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("A descrição é obrigatória.");

        if (descricao.Trim().Length > 255)
            throw new ArgumentException(
                "A descrição deve ter no máximo 255 caracteres.");

        _ = NormalizarTipo(tipoCategoria);

        if (valor <= 0)
            throw new ArgumentException("O valor deve ser maior que zero.");

        if (dataEmissao == default || dataVencimento == default)
            throw new ArgumentException(
                "As datas de emissão e vencimento são obrigatórias.");

        if (dataVencimento < dataEmissao)
            throw new ArgumentException(
                "O vencimento não pode ser anterior à emissão.");
    }

    private static string NormalizarTipo(string tipo)
    {
        var normalizado = (tipo ?? string.Empty).Trim().ToUpperInvariant();

        if (normalizado != "DESPESA")
            throw new ArgumentException(
                "O tipo da categoria de uma conta a pagar deve ser DESPESA.");

        return normalizado;
    }

    private static ContaPagarRespostaDto Mapear(ContaPagar conta)
    {
        return new ContaPagarRespostaDto
        {
            Id = conta.Id,
            IdEmpresa = conta.IdEmpresa,
            IdCategoria = conta.IdCategoria,
            TipoCategoria = conta.TipoCategoria,
            IdCentroCusto = conta.IdCentroCusto,
            Descricao = conta.Descricao,
            ValorOriginal = conta.ValorOriginal,
            DataEmissao = conta.DataEmissao,
            DataVencimento = conta.DataVencimento,
            DataCadastro = conta.DataCadastro,
            Cancelada = conta.Cancelada
        };
    }
}