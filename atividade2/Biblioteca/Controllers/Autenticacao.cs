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

        public static bool AutenticaUsuario(string nome, string senha, Controller controller)
        {   
            UsuarioService us = new UsuarioService();
            Usuario u = new Usuario();

            using(MD5 md5Hash = MD5.Create())
            {
                string hash = Criptografia.Encriptar(md5Hash, senha);
                senha = hash;
            }

            u = us.ObterUsuario(nome, senha);
            if(u != null)
            {
                controller.HttpContext.Session.SetString("user", nome);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}