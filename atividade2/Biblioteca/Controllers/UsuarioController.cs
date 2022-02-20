using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario u)
        {
            UsuarioService usuarioService = new UsuarioService();

            if(u.Id == 0)
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = usuarioService.Encriptar(md5Hash, u.Senha);
                    u.Senha = hash;
                }
                
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
            Autenticacao.CheckLogin(this);

            UsuarioService usuarioService = new UsuarioService();
            return View(usuarioService.Listar());
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            UsuarioService us = new UsuarioService();
            Usuario u = us.ObterPorId(id);
            return View(u);
        }
    }
}