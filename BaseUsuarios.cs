using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public static class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario{nome = "Fulano", senha = "123456",
                        funcoes = new string[]{ Funcao.Aluno} },
                new Usuario { nome = "Beltrano", senha = "123456",
                        funcoes = new string[]{ Funcao.Professor}},
                new Usuario { nome = "Ciclano", senha = "123456",
                        funcoes = new string[]{ Funcao.Professor, Funcao.Administrador} }
            };
        }

    }

    public class Usuario
    {
        public string nome { get; set; }
        public string senha { get; set; }
        public string[] funcoes { get; set; }
    }

    public class Funcao
    {
        public const string Aluno = "Aluno";
        public const string Professor = "Professor";
        public const string Administrador = "Administrador";
    }
}