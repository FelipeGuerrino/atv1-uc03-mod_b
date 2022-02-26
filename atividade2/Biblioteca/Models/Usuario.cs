namespace Biblioteca.Models 
{
    public class Usuario 
    {
        public Usuario() {}
        public Usuario(string nome, string senha, int id)
        {
            this.Nome = nome;
            this.Senha = senha;
            this.Id = id;
        }

        public static int ADMIN = 0;
        public static int PADRAO = 1;
        public int Id {get; set;}
        public string Nome {get; set;}
        public string Senha {get; set;}
        public int Tipo {get; set;}
    }
}