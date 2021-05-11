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
    public class ClienteCatalogoTipoMidiaServico : IModeloCRUDInterface<ClienteCatalogoTipoMidia, ClienteCatalogoTipoMidia>
    {
        private readonly ClienteCatalogoTipoMidiaRepositorio _repositorio;
        public ClienteCatalogoTipoMidiaServico()
        {
        }
        public ClienteCatalogoTipoMidiaServico(DataContext contexto)
        {
            _repositorio = new ClienteCatalogoTipoMidiaRepositorio(contexto);
        }

        public async Task<ClienteCatalogoTipoMidia> Incluir(ClienteCatalogoTipoMidia catalogoTipoMidia)
        {
            return await _repositorio.Incluir(catalogoTipoMidia);
        }
        public async Task<ClienteCatalogoTipoMidia> Alterar(ClienteCatalogoTipoMidia catalogoTipoMidia)
        {
            return await _repositorio.Alterar(catalogoTipoMidia);
        }
        public async Task<ClienteCatalogoTipoMidia> Excluir(int id)
        {
            return await _repositorio.Excluir(id);
        }
        public async Task<List<ClienteCatalogoTipoMidia>> Listar()
        {            
            return await _repositorio.Listar();
        }
        public async Task<List<ClienteCatalogoTipoMidia>> Pesquisar(int idCliente)
        {
            Expression<Func<ClienteCatalogoTipoMidia, bool>> where = f => true;
            if (idCliente != 0)
            {
                where = Util.ExpressionCombiner.And<ClienteCatalogoTipoMidia>(where, c => c.IdCliente == idCliente);
            }
            List<ClienteCatalogoTipoMidia> catalogoTipoMidia = await _repositorio.Listar(where);
            return catalogoTipoMidia;
        }
        public async Task<ClienteCatalogoTipoMidia> BuscarId(int id)
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
