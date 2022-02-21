using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Autenticacao.CheckLogin(this);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        //TODO: diferenciar sessão de admin para outros usuários
        {
            UsuarioService us = new UsuarioService();

            using (MD5 md5Hash = MD5.Create())
                {
                    string hash = us.Encriptar(md5Hash, senha);
                    senha = hash;
                }
            if(login != null && senha != null)
            {
                Usuario u =  new Usuario();
                u = us.ObterUsuario(login, senha);
                                
                if(u != null)
                {
                    HttpContext.Session.SetString("user", login);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Erro"] = "Senha inválida";
                    return View();
                }
            }
            else
            {
                ViewData["Erro"] = "Preencha todos os campos";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
