using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class UsuarioService 
    {
        public void Inserir(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(u);


                bc.SaveChanges();
            }
        }

        public void Atualizar(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario user = bc.Usuarios.Find(u.Id);
                user.Nome = u.Nome;
                user.Senha = u.Senha;
                
                bc.SaveChanges();
            }
        }

        public ICollection<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Usuario> query;

                query = bc.Usuarios;

                return query.OrderBy(u => u.Nome).ToList();
            }
        }

        public Usuario ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public Usuario ObterUsuario(string nome, string senha)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
            }
        }

        public string Encriptar(MD5 md5Hash, string senha)
        {
            byte[] dado = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < dado.Length; i++)
            {
                sBuilder.Append(dado[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}