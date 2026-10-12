
using Finity.Api.Data;
using Finity.Api.Dtos.Perfis;
using Finity.Api.Interfaces;
using Finity.Api.Models.Cadastro;
using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Services;

public class PerfilService : IPerfilService
{
    private readonly AppDbContext _context;

    public PerfilService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PerfilRespostaDto>> ListarAsync()
    {
        return await _context.Perfis
            .AsNoTracking()
            .OrderBy(p => p.Nome)
            .Select(p => new PerfilRespostaDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Ativo = p.Ativo
            })
            .ToListAsync();
    }

    public async Task<PerfilRespostaDto?> BuscarPorIdAsync(int id)
    {
        return await _context.Perfis
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PerfilRespostaDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Ativo = p.Ativo
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PerfilRespostaDto> CriarAsync(CriarPerfilDto dto)
    {
        var nome = dto.Nome.Trim();

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do perfil é obrigatório.");

        var existe = await _context.Perfis
            .AnyAsync(p => p.Nome == nome);

        if (existe)
            throw new InvalidOperationException(
                "Já existe um perfil com esse nome.");

        var perfil = new Perfil
        {
            Nome = nome,
            Descricao = LimparTexto(dto.Descricao),
            Ativo = true
        };

        _context.Perfis.Add(perfil);
        await _context.SaveChangesAsync();

        return MapearResposta(perfil);
    }

    public async Task<PerfilRespostaDto?> AtualizarAsync(
        int id,
        AtualizarPerfilDto dto)
    {
        var perfil = await _context.Perfis
            .FirstOrDefaultAsync(p => p.Id == id);

        if (perfil is null)
            return null;

        var nome = dto.Nome.Trim();

        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do perfil é obrigatório.");

        var existe = await _context.Perfis
            .AnyAsync(p => p.Id != id && p.Nome == nome);

        if (existe)
            throw new InvalidOperationException(
                "Outro perfil já utiliza esse nome.");

        perfil.Nome = nome;
        perfil.Descricao = LimparTexto(dto.Descricao);
        perfil.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        return MapearResposta(perfil);
    }

    private static string? LimparTexto(string? texto)
    {
        return string.IsNullOrWhiteSpace(texto)
            ? null
            : texto.Trim();
    }

    private static PerfilRespostaDto MapearResposta(Perfil perfil)
    {
        return new PerfilRespostaDto
        {
            Id = perfil.Id,
            Nome = perfil.Nome,
            Descricao = perfil.Descricao,
            Ativo = perfil.Ativo
        };
    }
}
