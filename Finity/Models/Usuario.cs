using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finity.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Cargo { get; set; }
        public string NivelAcesso { get; set; }

        public int IdDepartamento { get; set; }
        public Departamento Departamento { get; set; }

        public string StatusUsuario { get; set; }
        public string Matricula { get; set; }

        public List<Assinatura> Assinaturas { get; set; }
    }
}
