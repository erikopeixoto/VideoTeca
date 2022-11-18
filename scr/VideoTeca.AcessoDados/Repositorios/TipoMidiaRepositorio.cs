using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class TipoMidiaRepositorio: GenericoRepositorio<TipoMidia>
    {
        protected readonly DataContext _contexto;
        public TipoMidiaRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.TipoMidias.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
