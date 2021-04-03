using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class CatalogoRepositorio: GenericoRepositorio<Catalogo>
    {
        protected readonly DataContext _contexto;
        public CatalogoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public bool ExisteCodigo(string codCodigo)
        {
            bool retorno = _contexto.Catalogos.Any(e => e.Codigo == codCodigo);
            return retorno;
        }
        public bool ExisteCodigo(string codCodigo, int id)
        {
            bool retorno = _contexto.Catalogos.Any(e => e.Codigo == codCodigo && e.Id != id);
            return retorno;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.Catalogos.Any(e => e.Id == id);
            return retorno;
        }
    }
 }
