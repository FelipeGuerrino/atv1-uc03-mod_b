using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        {
            UsuarioService us = new UsuarioService();

            if(login != null && senha != null)
            {
                var u = us.ObterUsuario(login, senha);
                
                if(u != null)
                {
                    HttpContext.Session.SetString("user", "admin");
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
