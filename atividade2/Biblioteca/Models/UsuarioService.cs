using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Biblioteca.Models
{
    public class UsuarioService 
    {
        public void Inserir(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Add(u);

                string hash = Criptografia.Encriptar(u.Senha);
                u.Senha = hash;

                bc.SaveChanges();
            }
        }

        public void Atualizar(Usuario u)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario user = bc.Usuarios.Find(u.Id);

                if(u.Senha != null)
                {
                    string hash = Criptografia.Encriptar(u.Senha);
                    user.Senha = hash;
                }
                user.Nome = u.Nome;
                user.Tipo = u.Tipo;
                bc.SaveChanges();
            }
        }

        public void Excluir(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Remove(bc.Usuarios.Find(id));
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
                if(!bc.Usuarios.Any())
                {
                    Inserir(new Usuario("admin", "123", 0));
                }
                return bc.Usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
            }
        }
    }
}