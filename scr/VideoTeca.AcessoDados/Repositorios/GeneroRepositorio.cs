using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class GeneroRepositorio : GenericoRepositorio<Genero>
    {
        protected readonly DataContext _contexto;
        public GeneroRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.Generos.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
