using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoTeca.AcessoDados.Contexto;
using VideoTeca.AcessoDados.Repositorios;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Interfaces;

namespace VideoTeca.Servicos.Servicos
{
    public class CatalogoTipoMidiaServico : ICatalogoTipoMidiaInterface
    {
        private readonly CatalogoTipoMidiaRepositorio _repositorio;
        private readonly IMapper _map;
        private readonly DataContext _contexto;
        public CatalogoTipoMidiaServico()
        {
        }
        public CatalogoTipoMidiaServico(DataContext contexto, IMapper mapper)
        {
            _repositorio = new CatalogoTipoMidiaRepositorio(contexto);
            _contexto = contexto;
            _map = mapper;
        }

        public async Task<CatalogoTipoMidia> Incluir(CatalogoTipoMidia catalogoTipoMidia)
        {
            catalogoTipoMidia.QtdDisponivel = catalogoTipoMidia.QtdTitulo;
            return await _repositorio.Incluir(catalogoTipoMidia);
        }
        public async Task<CatalogoTipoMidia> Alterar(CatalogoTipoMidia catalogoTipoMidia)
        {
            CatalogoTipoMidia catalogo = this.BuscarId(catalogoTipoMidia.Id).Result;
            int diferenca;
            if (catalogoTipoMidia.QtdTitulo > catalogo.QtdTitulo)
            {
                diferenca = (catalogoTipoMidia.QtdTitulo - catalogo.QtdTitulo);
                catalogoTipoMidia.QtdDisponivel = catalogoTipoMidia.QtdDisponivel + diferenca;
            }
            else if (catalogoTipoMidia.QtdTitulo < catalogo.QtdTitulo)
            {
                diferenca = (catalogo.QtdTitulo - catalogoTipoMidia.QtdTitulo);
                catalogoTipoMidia.QtdDisponivel = catalogoTipoMidia.QtdDisponivel - diferenca;
                if (catalogoTipoMidia.QtdDisponivel < 0)
                {
                    throw new ArgumentException("Existem catálogos locados que impedem essa alteração.");
                }
            }
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
        public int? ExisteCódigo(int idCatalogo, int idtipoMidia)
        {
            return _repositorio.ExisteCodigo(idCatalogo, idtipoMidia);
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
