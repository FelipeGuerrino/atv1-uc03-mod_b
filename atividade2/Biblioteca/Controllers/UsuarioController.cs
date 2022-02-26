using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            if(Autenticacao.IsAdmin(this))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();

            if(u.Id == 0)
            {
                usuarioService.Inserir(u);
            }
            else
            {
                usuarioService.Atualizar(u);
            }

            return View();
        }

        public IActionResult Listagem()
        {
            if(Autenticacao.IsAdmin(this))
            {
                UsuarioService usuarioService = new UsuarioService();
                return View(usuarioService.Listar());
            }
            else
            {
                return Redirect("~/Home/Index");
            }

        }

        public IActionResult Edicao(int id)
        {
            if(Autenticacao.IsAdmin(this))
            {
                UsuarioService us = new UsuarioService();
                Usuario u = us.ObterPorId(id);
                return View(u);
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }

        public IActionResult Excluir(int id)
        {
            if(Autenticacao.IsAdmin(this))
            {
                UsuarioService us = new UsuarioService();
                us.Excluir(id);
                return Redirect("Usuario/Listagem");   
            }
            else
            {
                return Redirect("~/Home/Index");
            }
        }
    }
}