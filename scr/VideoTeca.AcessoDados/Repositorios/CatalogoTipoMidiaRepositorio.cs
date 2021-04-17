using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class CatalogoTipoMidiaRepositorio: GenericoRepositorio<CatalogoTipoMidia>
    {
        protected readonly DataContext _contexto;
        public CatalogoTipoMidiaRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.CatalogoTipoMidias.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
