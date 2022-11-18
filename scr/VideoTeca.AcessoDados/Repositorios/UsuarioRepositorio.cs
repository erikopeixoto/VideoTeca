using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class UsuarioRepositorio : GenericoRepositorio<Usuario>
    {
        protected readonly DataContext _contexto;
        public UsuarioRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public static Usuario Get(string username, string password)
        {
            var users = new List<Usuario>();
            users.Add(new Usuario { Id = 1, Nome = "batman", Senha = "batman" });
            users.Add(new Usuario { Id = 2, Nome = "robin", Senha = "robin"});
            return users.Where(x => x.Nome.ToLower() == username.ToLower() && x.Senha == x.Senha).FirstOrDefault();
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.Generos.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
