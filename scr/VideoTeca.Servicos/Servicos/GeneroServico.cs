using VideoTeca.Servicos.Interfaces;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Modelos.Dtos;
using VideoTeca.AcessoDados.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using VideoTeca.AcessoDados.Contexto;
using VideoTeca.Servicos.Validar;
using System;

namespace VideoTeca.Servicos.Servicos
{ 
    public class GeneroServico : IModeloCRUDInterface<Genero, Genero>
    {
        private readonly GeneroRepositorio repositorio;
        public GeneroServico()
        {
        }
        public GeneroServico(DataContext contexto)
        {
            repositorio = new GeneroRepositorio(contexto);
        }

        public async Task<Genero> Incluir(Genero genero)
        {
            this.ValidarGenero(genero);
            return await repositorio.Incluir(genero);
        }
        public async Task<Genero> Alterar(Genero genero)
        {
            return await repositorio.Alterar(genero.Id, genero);
        }
        public async Task<Genero> Excluir(int id)
        {
            return await repositorio.Excluir(id);
        }
        public async Task<List<Genero>> Listar()
        {            
            return await repositorio.Listar();
        }
        public async Task<Genero> BuscarId(int id)
        {
            return await repositorio.BuscarId(id); ;
        }
        public bool Existe(int id)
        {
            return repositorio.Existe(id);
        }
        private void ValidarGenero(Genero genero)
        {
            string retorno = GeneroValidar.ValidarGenero(genero);
            if (!string.IsNullOrEmpty(retorno))
            {
                throw new ArgumentException(retorno);
            }
        }
        public Genero TesteGenero(Genero genero)
        {
            Genero retorno = new Genero();
            string validar = GeneroValidar.ValidarGenero(genero);
            if (string.IsNullOrEmpty(validar))
            {
                retorno = genero;
            }
            return retorno;
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
