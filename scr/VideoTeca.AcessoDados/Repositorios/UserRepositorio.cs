using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class UserRepositorio : GenericoRepositorio<User>
    {
        protected readonly DataContext _contexto;
        public UserRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, UserName = "batman", PasswordHash = "batman" });
            users.Add(new User { Id = 2, UserName = "robin", PasswordHash = "robin"});
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.PasswordHash == x.PasswordHash).FirstOrDefault();
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.Generos.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
