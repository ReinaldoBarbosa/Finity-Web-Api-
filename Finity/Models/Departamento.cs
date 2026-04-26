namespace Finity.Models
{
    public class Departamento
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
