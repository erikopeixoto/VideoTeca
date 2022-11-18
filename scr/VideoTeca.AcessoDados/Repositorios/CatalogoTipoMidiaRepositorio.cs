using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class CatalogoTipoMidiaRepositorio: GenericoRepositorio<CatalogoTipoMidia>
    {
        protected readonly DataContext _contexto;
        public CatalogoTipoMidiaRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public override async Task<CatalogoTipoMidia> BuscarId(int id)
        {
            return _contexto.CatalogoTipoMidias.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }
        public int? ExisteCodigo(int idCatalogo, int idTipoMidia)
        {
            int retorno = 0;
            CatalogoTipoMidia catalogo = _contexto.CatalogoTipoMidias
                .AsNoTracking()
                .FirstOrDefault(e => e.IdCatalogo == idCatalogo && e.IdTipoMidia == idTipoMidia);
            if (catalogo != null)
            {
                retorno = catalogo.Id;
            }
            catalogo = null;
            return retorno;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.CatalogoTipoMidias.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
