using VideoTeca.Servicos.Interfaces;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Modelos.Dtos;
using VideoTeca.AcessoDados.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using VideoTeca.AcessoDados.Contexto;
using System.Linq.Expressions;
using AutoMapper;
using System;
using VideoTeca.Servicos.Validar;

namespace VideoTeca.Servicos.Servicos
{ 
    public class CatalogoTipoMidiaServico : IModeloCRUDInterface<CatalogoTipoMidia, CatalogoTipoMidia>
    {
        private readonly CatalogoTipoMidiaRepositorio _repositorio;
        public CatalogoTipoMidiaServico()
        {
        }
        public CatalogoTipoMidiaServico(DataContext contexto)
        {
            _repositorio = new CatalogoTipoMidiaRepositorio(contexto);
        }

        public async Task<CatalogoTipoMidia> Incluir(CatalogoTipoMidia catalogoTipoMidia)
        {
            return await _repositorio.Incluir(catalogoTipoMidia);
        }
        public async Task<CatalogoTipoMidia> Alterar(CatalogoTipoMidia catalogoTipoMidia)
        {
            return await _repositorio.Alterar(catalogoTipoMidia);
        }
        public async Task<CatalogoTipoMidia> Excluir(int id)
        {
            return await _repositorio.Excluir(id);
        }
        public async Task<List<CatalogoTipoMidia>> Listar()
        {            
            return await _repositorio.Listar();
        }
        public async Task<List<CatalogoTipoMidia>> Pesquisar(int idCatalogo)
        {
            Expression<Func<CatalogoTipoMidia, bool>> where = f => true;
            if (idCatalogo != 0)
            {
                where = Util.ExpressionCombiner.And<CatalogoTipoMidia>(where, c => c.IdCatalogo == idCatalogo);
            }
            List<CatalogoTipoMidia> catalogoTipoMidia = await _repositorio.Listar(where);
            return catalogoTipoMidia;
        }
        public async Task<CatalogoTipoMidia> BuscarId(int id)
        {
            return await _repositorio.BuscarId(id); ;
        }
        public bool Existe(int id)
        {
            return _repositorio.Existe(id);
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
