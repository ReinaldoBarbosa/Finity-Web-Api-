using Finity.Api.Data;
using Finity.Api.Dtos.CentrosCusto;
using Finity.Api.Interfaces;
using Finity.Api.Models.Cadastro;
using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Services;

public class CentroCustoService : ICentroCustoService
{
    private readonly AppDbContext _context;

    public CentroCustoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CentroCustoRespostaDto>> ListarAsync()
    {
        return await _context.CentrosCusto
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .Select(c => new CentroCustoRespostaDto
            {
                Id = c.Id,
                IdEmpresa = c.IdEmpresa,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Ativo = c.Ativo,
                DataCadastro = c.DataCadastro
            })
            .ToListAsync();
    }

    public async Task<CentroCustoRespostaDto?> BuscarPorIdAsync(int id)
    {
        var centro = await _context.CentrosCusto
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return centro is null ? null : Mapear(centro);
    }

    public async Task<CentroCustoRespostaDto> CriarAsync(
        CriarCentroCustoDto dto)
    {
        var nome = dto.Nome.Trim();

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do centro de custo é obrigatório.");

        var empresa = await _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == dto.IdEmpresa);

        if (empresa is null)
            throw new ArgumentException("A empresa informada não existe.");

        if (!empresa.Ativo)
            throw new InvalidOperationException(
                "A empresa informada está inativa.");

        var duplicado = await _context.CentrosCusto.AnyAsync(c =>
            c.IdEmpresa == dto.IdEmpresa &&
            c.Nome.ToLower() == nome.ToLower());

        if (duplicado)
            throw new InvalidOperationException(
                "Já existe um centro de custo com esse nome para esta empresa.");

        var centro = new CentroCusto
        {
            IdEmpresa = dto.IdEmpresa,
            Nome = nome,
            Descricao = string.IsNullOrWhiteSpace(dto.Descricao)
                ? null
                : dto.Descricao.Trim(),
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        _context.CentrosCusto.Add(centro);
        await _context.SaveChangesAsync();

        return Mapear(centro);
    }

    public async Task<CentroCustoRespostaDto?> AtualizarAsync(
        int id, AtualizarCentroCustoDto dto)
    {
        var centro = await _context.CentrosCusto
            .FirstOrDefaultAsync(c => c.Id == id);

        if (centro is null)
            return null;

        var nome = dto.Nome.Trim();

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do centro de custo é obrigatório.");

        var duplicado = await _context.CentrosCusto.AnyAsync(c =>
            c.IdEmpresa == centro.IdEmpresa &&
            c.Id != id &&
            c.Nome.ToLower() == nome.ToLower());

        if (duplicado)
            throw new InvalidOperationException(
                "Já existe um centro de custo com esse nome para esta empresa.");

        centro.Nome = nome;
        centro.Descricao = string.IsNullOrWhiteSpace(dto.Descricao)
            ? null
            : dto.Descricao.Trim();
        centro.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        return Mapear(centro);
    }

    private static CentroCustoRespostaDto Mapear(CentroCusto centro)
    {
        return new CentroCustoRespostaDto
        {
            Id = centro.Id,
            IdEmpresa = centro.IdEmpresa,
            Nome = centro.Nome,
            Descricao = centro.Descricao,
            Ativo = centro.Ativo,
            DataCadastro = centro.DataCadastro
        };
    }
}