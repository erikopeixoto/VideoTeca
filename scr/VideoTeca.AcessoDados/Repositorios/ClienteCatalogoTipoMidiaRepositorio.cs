using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class ClienteCatalogoTipoMidiaRepositorio: GenericoRepositorio<ClienteCatalogoTipoMidia>
    {
        protected readonly DataContext _contexto;
        public ClienteCatalogoTipoMidiaRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.ClienteCatalogoTipoMidias.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
