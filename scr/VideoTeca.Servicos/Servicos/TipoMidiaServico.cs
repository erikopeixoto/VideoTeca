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
    public class TipoMidiaServico : IModeloCRUDInterface<TipoMidia, TipoMidia>
    {
        private readonly TipoMidiaRepositorio repositorio;
        public TipoMidiaServico()
        {
        }
        public TipoMidiaServico(DataContext contexto)
        {
            repositorio = new TipoMidiaRepositorio(contexto);
        }

        public async Task<TipoMidia> Incluir(TipoMidia tipoMidia)
        {
            return await repositorio.Incluir(tipoMidia);
        }
        public async Task<TipoMidia> Alterar(TipoMidia tipoMidia)
        {
            return await repositorio.Alterar(tipoMidia.Id, tipoMidia);
        }
        public async Task<TipoMidia> Excluir(int id)
        {
            return await repositorio.Excluir(id);
        }
        public async Task<List<TipoMidia>> Listar()
        {            
            return await repositorio.Listar();
        }
        public async Task<TipoMidia> BuscarId(int id)
        {
            return await repositorio.BuscarId(id); ;
        }
        public bool Existe(int id)
        {
            return repositorio.Existe(id);
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        private void ValidarTipoMidia(TipoMidia tipoMidia)
        {
            string retorno = TipoMidiaValidar.ValidarTipoMidia(tipoMidia);
            if (!string.IsNullOrEmpty(retorno))
            {
                throw new ArgumentException(retorno);
            }
        }
        public TipoMidia TesteTipoMidia(TipoMidia tipoMidia)
        {
            TipoMidia retorno = new TipoMidia();
            string validar = TipoMidiaValidar.ValidarTipoMidia(tipoMidia);
            if (string.IsNullOrEmpty(validar))
            {
                retorno = tipoMidia;
            }
            return retorno;
        }
    }
}
