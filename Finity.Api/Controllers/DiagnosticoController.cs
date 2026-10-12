
using Finity.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

using System.Linq;
using System.Collections.Generic;

namespace Finity.Api.Controllers;

[ApiController]
[Route("api/diagnostico")]
public class DiagnosticoController : ControllerBase
{
    private readonly AppDbContext _context;

    public DiagnosticoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("banco")]
    public async Task<IActionResult> TestarBanco()
    {
        try
        {
            var conexao = _context.Database.GetDbConnection();

            await _context.Database.OpenConnectionAsync();

            try
            {
                await using var comando = conexao.CreateCommand();
                comando.CommandText = "SELECT DB_NAME()";

                var resultado = await comando.ExecuteScalarAsync();
                var nomeBanco = resultado?.ToString();

                return Ok(new
                {
                    sucesso = true,
                    mensagem = "Conexão com o banco estabelecida.",
                    banco = nomeBanco
                });
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }
        catch (SqlException)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                mensagem = "Não foi possível conectar ao SQL Server. Confira a conexão e os User Secrets."
            });
        }
        catch (InvalidOperationException)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                mensagem = "Não foi possível inicializar a conexão. Confira a configuração do AppDbContext."
            });
        }
    }


    [HttpGet("entidades")]
    public async Task<IActionResult> TestarEntidades()
    {
        try
        {
            var resultado = new
            {
                Empresas = await _context.Empresas.CountAsync(),
                Perfis = await _context.Perfis.CountAsync(),
                Usuarios = await _context.Usuarios.CountAsync(),
                Categorias = await _context.Categorias.CountAsync(),
                CentrosCusto = await _context.CentrosCusto.CountAsync(),
                ContasPagar = await _context.ContasPagar.CountAsync(),
                ContasReceber = await _context.ContasReceber.CountAsync(),
                Movimentacoes = await _context.Movimentacoes.CountAsync(),
                Pagamentos = await _context.PagamentosContaPagar.CountAsync(),
                Recebimentos = await _context.RecebimentosContaReceber.CountAsync(),
                HistoricosPagar = await _context.HistoricoVencimentosContaPagar.CountAsync(),
                HistoricosReceber = await _context.HistoricoVencimentosContaReceber.CountAsync()
            };

            return Ok(new
            {
                sucesso = true,
                mensagem = "As 12 entidades foram consultadas.",
                tabelas = resultado
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                erro = ex.GetType().Name,
                mensagem = "Falha ao consultar as entidades. Verifique o log da aplicação."
            });
        }
    }


    [HttpGet("views")]
    public async Task<IActionResult> TestarViews()
    {
        var nomesEsperados = new[]
        {
        "vw_ContasPagarResumo",
        "vw_ContasReceberResumo",
        "vw_ResumoFinanceiro",
        "vw_MovimentacoesDetalhadas",
        "vw_ResumoPorCategoria",
        "vw_DashboardFinanceiro"
    };

        var existentes = new List<string>();

        var conexao = _context.Database.GetDbConnection();
        await _context.Database.OpenConnectionAsync();

        try
        {
            await using var comando = conexao.CreateCommand();
            comando.CommandText = """
            SELECT name
            FROM sys.views
            WHERE name IN (
                'vw_ContasPagarResumo',
                'vw_ContasReceberResumo',
                'vw_ResumoFinanceiro',
                'vw_MovimentacoesDetalhadas',
                'vw_ResumoPorCategoria',
                'vw_DashboardFinanceiro'
            )
            ORDER BY name;
            """;

            await using var leitor = await comando.ExecuteReaderAsync();

            while (await leitor.ReadAsync())
            {
                existentes.Add(leitor.GetString(0));
            }
        }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }

        return Ok(new
        {
            sucesso = nomesEsperados.All(existentes.Contains),
            esperadas = nomesEsperados.Length,
            encontradas = existentes.Count,
            views = existentes,
            ausentes = nomesEsperados.Except(existentes).ToArray()
        });
    }


    [HttpGet("views/colunas")]
    public async Task<IActionResult> ListarColunasViews()
    {
        var colunas = new List<object>();

        var conexao = _context.Database.GetDbConnection();
        await _context.Database.OpenConnectionAsync();

        try
        {
            await using var comando = conexao.CreateCommand();

            comando.CommandText = """
            SELECT
                v.name AS view_name,
                c.column_id,
                c.name AS column_name,
                t.name AS type_name,
                c.max_length,
                c.precision,
                c.scale,
                c.is_nullable
            FROM sys.views v
            INNER JOIN sys.columns c ON c.object_id = v.object_id
            INNER JOIN sys.types t ON t.user_type_id = c.user_type_id
            WHERE v.name IN (
                'vw_ContasPagarResumo',
                'vw_ContasReceberResumo',
                'vw_ResumoFinanceiro',
                'vw_MovimentacoesDetalhadas',
                'vw_ResumoPorCategoria',
                'vw_DashboardFinanceiro'
            )
            ORDER BY v.name, c.column_id;
            """;

            await using var leitor = await comando.ExecuteReaderAsync();

            while (await leitor.ReadAsync())
            {
                colunas.Add(new
                {
                    view = leitor.GetString(0),
                    ordem = leitor.GetInt32(1),
                    coluna = leitor.GetString(2),
                    tipo = leitor.GetString(3),
                    tamanho = leitor.GetInt16(4),
                    precisao = leitor.GetByte(5),
                    escala = leitor.GetByte(6),
                    aceitaNulo = leitor.GetBoolean(7)
                });
            }
        }
        finally
        {
            await _context.Database.CloseConnectionAsync();
        }

        return Ok(new
        {
            sucesso = true,
            totalColunas = colunas.Count,
            colunas
        });
    }


    [HttpGet("dashboard")]
    public async Task<IActionResult> TestarDashboard()
    {
        var dados = await _context.DashboardFinanceiro
            .AsNoTracking()
            .ToListAsync();

        return Ok(new
        {
            sucesso = true,
            quantidade = dados.Count,
            dados
        });
    }


    [HttpGet("views/testar-todas")]
    public async Task<IActionResult> TestarTodasViews()
    {
        var resultado = new Dictionary<string, object>();

        try
        {
            resultado["vw_ContasPagarResumo"] =
                await _context.ContasPagarResumo
                    .AsNoTracking()
                    .CountAsync();

            resultado["vw_ContasReceberResumo"] =
                await _context.ContasReceberResumo
                    .AsNoTracking()
                    .CountAsync();

            resultado["vw_DashboardFinanceiro"] =
                await _context.DashboardFinanceiro
                    .AsNoTracking()
                    .CountAsync();

            resultado["vw_MovimentacoesDetalhadas"] =
                await _context.MovimentacoesDetalhadas
                    .AsNoTracking()
                    .CountAsync();

            resultado["vw_ResumoFinanceiro"] =
                await _context.ResumosFinanceiros
                    .AsNoTracking()
                    .CountAsync();

            resultado["vw_ResumoPorCategoria"] =
                await _context.ResumosPorCategoria
                    .AsNoTracking()
                    .CountAsync();

            return Ok(new
            {
                sucesso = true,
                mensagem = "Todas as views foram consultadas.",
                quantidades = resultado
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                sucesso = false,
                erro = ex.Message
            });
        }
    }

}
