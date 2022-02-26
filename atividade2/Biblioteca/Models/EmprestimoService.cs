using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Models
{
    public class EmprestimoService 
    {
        [ViewData]
        public static string Enfase {get; set;}
        public void Inserir(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Emprestimos.Add(e);
                bc.SaveChanges();
            }
        }

        public void Atualizar(Emprestimo e)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Emprestimo emprestimo = bc.Emprestimos.Find(e.Id);
                emprestimo.NomeUsuario = e.NomeUsuario;
                emprestimo.Telefone = e.Telefone;
                emprestimo.LivroId = e.LivroId;
                emprestimo.DataEmprestimo = e.DataEmprestimo;
                emprestimo.DataDevolucao = e.DataDevolucao;

                bc.SaveChanges();
            }
        }

        public ICollection<Emprestimo> ListarTodos(int page, int size, FiltrosEmprestimos filtro)
        {
            int pular = (page - 1) * size;
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Include(e => e.Livro).Skip(pular).Take(size).ToList();
            }
        }

        public Emprestimo ObterPorId(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Find(id);
            }
        }

        public int CountEmprestimos()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Emprestimos.Count();
            }
        }

        public static void ChecarAtraso(int id)
        {
            Enfase = "";
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                var query = bc.Emprestimos.Find(id);
                if (DateTime.Now.CompareTo(query.DataDevolucao) > 0)
                {
                    Enfase = "text-danger";
                }
            }
        }
    }
}