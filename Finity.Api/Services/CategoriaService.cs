
using Finity.Api.Data;
using Finity.Api.Dtos.Categorias;
using Finity.Api.Interfaces;
using Finity.Api.Models.Cadastro;
using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Services;

public class CategoriaService : ICategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoriaRespostaDto>> ListarAsync()
    {
        return await _context.Categorias
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .Select(c => new CategoriaRespostaDto
            {
                Id = c.Id,
                IdEmpresa = c.IdEmpresa,
                Nome = c.Nome,
                Descricao = c.Descricao,
                Tipo = c.Tipo,
                Ativo = c.Ativo,
                DataCadastro = c.DataCadastro
            })
            .ToListAsync();
    }

    public async Task<CategoriaRespostaDto?> BuscarPorIdAsync(int id)
    {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return categoria is null ? null : Mapear(categoria);
    }

    public async Task<CategoriaRespostaDto> CriarAsync(CriarCategoriaDto dto)
    {
        ValidarTipo(dto.Tipo);

        var empresa = await _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == dto.IdEmpresa);

        if (empresa is null)
            throw new ArgumentException("A empresa informada não existe.");

        if (!empresa.Ativo)
            throw new InvalidOperationException("A empresa informada está inativa.");

        var nome = dto.Nome.Trim();

        var tipo = dto.Tipo.Trim().ToUpperInvariant();

        var duplicada = await _context.Categorias.AnyAsync(c =>
            c.IdEmpresa == dto.IdEmpresa &&
            c.Tipo == tipo &&
            c.Nome.ToLower() == nome.ToLower());

        if (duplicada)
            throw new InvalidOperationException(
                "Já existe uma categoria com esse nome para esta empresa.");

        var categoria = new Categoria
        {
            IdEmpresa = dto.IdEmpresa,
            Nome = nome,
            Descricao = string.IsNullOrWhiteSpace(dto.Descricao)
                ? null
                : dto.Descricao.Trim(),
            Tipo = tipo,
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        return Mapear(categoria);
    }

    public async Task<CategoriaRespostaDto?> AtualizarAsync(
        int id, AtualizarCategoriaDto dto)
    {
        ValidarTipo(dto.Tipo);

        var categoria = await _context.Categorias
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria is null)
            return null;

        var nome = dto.Nome.Trim();

        var tipo = dto.Tipo.Trim().ToUpperInvariant();

        var duplicada = await _context.Categorias.AnyAsync(c =>
            c.IdEmpresa == categoria.IdEmpresa &&
            c.Id != id &&
            c.Tipo == tipo &&
            c.Nome.ToLower() == nome.ToLower());

        if (duplicada)
            throw new InvalidOperationException(
                "Já existe uma categoria com esse nome para esta empresa.");

        categoria.Nome = nome;
        categoria.Descricao = string.IsNullOrWhiteSpace(dto.Descricao)
            ? null
            : dto.Descricao.Trim();
        categoria.Tipo = tipo;
        categoria.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        return Mapear(categoria);
    }

    private static void ValidarTipo(string tipo)
    {
        var tipoNormalizado = tipo.Trim().ToUpperInvariant();

        if (tipoNormalizado is not ("DESPESA" or "RECEITA"))
        {
            throw new ArgumentException(
                "O tipo deve ser DESPESA ou RECEITA.");
        }
    }

    private static CategoriaRespostaDto Mapear(Categoria categoria)
    {
        return new CategoriaRespostaDto
        {
            Id = categoria.Id,
            IdEmpresa = categoria.IdEmpresa,
            Nome = categoria.Nome,
            Descricao = categoria.Descricao,
            Tipo = categoria.Tipo,
            Ativo = categoria.Ativo,
            DataCadastro = categoria.DataCadastro
        };
    }
}
