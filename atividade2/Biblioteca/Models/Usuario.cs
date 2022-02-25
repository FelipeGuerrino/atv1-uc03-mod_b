namespace Biblioteca.Models 
{
    public class Usuario 
    {
        public Usuario() {}
        public Usuario(string nome, string senha)
        {
            this.Nome = nome;
            this.Senha = senha;
        }
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Senha {get; set;}
    }
}