using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;
using System.Security.Cryptography;

namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
        public static void CheckLogin(Controller controller)
        {   
            if(string.IsNullOrEmpty(controller.HttpContext.Session.GetString("user")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }

        public static bool IsAdmin(Controller controller)
        {
            if(controller.HttpContext.Session.GetInt32("tipo") == Usuario.ADMIN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool AutenticaUsuario(string nome, string senha, Controller controller)
        {   
            UsuarioService us = new UsuarioService();
            Usuario u = new Usuario();

            string hash = Criptografia.Encriptar(senha);
            senha = hash;

            u = us.ObterUsuario(nome, senha);
            if(u != null)
            {
                controller.HttpContext.Session.SetString("user", u.Nome);
                controller.HttpContext.Session.SetInt32("tipo", u.Tipo);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}