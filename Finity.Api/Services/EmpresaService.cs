
using Finity.Api.Data;
using Finity.Api.Dtos.Empresas;
using Finity.Api.Interfaces;
using Finity.Api.Models.Cadastro;
using Microsoft.EntityFrameworkCore;

namespace Finity.Api.Services;

public class EmpresaService : IEmpresaService
{
    private readonly AppDbContext _context;

    public EmpresaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmpresaRespostaDto>> ListarAsync()
    {
        return await _context.Empresas
            .AsNoTracking()
            .OrderBy(e => e.RazaoSocial)
            .Select(e => new EmpresaRespostaDto
            {
                Id = e.Id,
                RazaoSocial = e.RazaoSocial,
                NomeFantasia = e.NomeFantasia,
                Cnpj = e.Cnpj,
                Email = e.Email,
                Telefone = e.Telefone,
                Ativo = e.Ativo,
                DataCadastro = e.DataCadastro
            })
            .ToListAsync();
    }

    public async Task<EmpresaRespostaDto?> BuscarPorIdAsync(int id)
    {
        return await _context.Empresas
            .AsNoTracking()
            .Where(e => e.Id == id)
            .Select(e => new EmpresaRespostaDto
            {
                Id = e.Id,
                RazaoSocial = e.RazaoSocial,
                NomeFantasia = e.NomeFantasia,
                Cnpj = e.Cnpj,
                Email = e.Email,
                Telefone = e.Telefone,
                Ativo = e.Ativo,
                DataCadastro = e.DataCadastro
            })
            .FirstOrDefaultAsync();
    }

    public async Task<EmpresaRespostaDto> CriarAsync(
        CriarEmpresaDto dto)
    {
        var cnpj = NormalizarCnpj(dto.Cnpj);

        if (!CnpjValido(cnpj))
            throw new ArgumentException(
                "O CNPJ deve conter 14 dígitos numéricos.");

        var cnpjExiste = await _context.Empresas
            .AnyAsync(e => e.Cnpj == cnpj);

        if (cnpjExiste)
            throw new InvalidOperationException(
                "Já existe uma empresa cadastrada com esse CNPJ.");

        var empresa = new Empresa
        {
            RazaoSocial = dto.RazaoSocial.Trim(),
            NomeFantasia = LimparTexto(dto.NomeFantasia),
            Cnpj = cnpj,
            Email = LimparTexto(dto.Email),
            Telefone = LimparTexto(dto.Telefone),
            Ativo = true,
            DataCadastro = DateTime.Now
        };

        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();

        return MapearResposta(empresa);
    }

    public async Task<EmpresaRespostaDto?> AtualizarAsync(
        int id,
        AtualizarEmpresaDto dto)
    {
        var empresa = await _context.Empresas
            .FirstOrDefaultAsync(e => e.Id == id);

        if (empresa is null)
            return null;

        var cnpj = NormalizarCnpj(dto.Cnpj);

        if (!CnpjValido(cnpj))
            throw new ArgumentException(
                "O CNPJ deve conter 14 dígitos numéricos.");

        var cnpjExiste = await _context.Empresas
            .AnyAsync(e => e.Id != id && e.Cnpj == cnpj);

        if (cnpjExiste)
            throw new InvalidOperationException(
                "Outra empresa já está cadastrada com esse CNPJ.");

        empresa.RazaoSocial = dto.RazaoSocial.Trim();
        empresa.NomeFantasia = LimparTexto(dto.NomeFantasia);
        empresa.Cnpj = cnpj;
        empresa.Email = LimparTexto(dto.Email);
        empresa.Telefone = LimparTexto(dto.Telefone);
        empresa.Ativo = dto.Ativo;

        await _context.SaveChangesAsync();

        return MapearResposta(empresa);
    }

    private static string NormalizarCnpj(string? cnpj)
    {
        return new string((cnpj ?? string.Empty)
            .Where(char.IsDigit)
            .ToArray());
    }

    private static bool CnpjValido(string cnpj)
    {
        return cnpj.Length == 14 && cnpj.All(char.IsDigit);
    }

    private static string? LimparTexto(string? texto)
    {
        return string.IsNullOrWhiteSpace(texto)
            ? null
            : texto.Trim();
    }

    private static EmpresaRespostaDto MapearResposta(Empresa empresa)
    {
        return new EmpresaRespostaDto
        {
            Id = empresa.Id,
            RazaoSocial = empresa.RazaoSocial,
            NomeFantasia = empresa.NomeFantasia,
            Cnpj = empresa.Cnpj,
            Email = empresa.Email,
            Telefone = empresa.Telefone,
            Ativo = empresa.Ativo,
            DataCadastro = empresa.DataCadastro
        };
    }
}
