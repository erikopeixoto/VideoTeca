using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.AcessoDados.Contexto;
using VideoTeca.AcessoDados.Repositorios;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Servicos.Interfaces;
using VideoTeca.Servicos.Validar;

namespace VideoTeca.Servicos.Servicos
{
    public class GeneroServico : IModeloCRUDInterface<GeneroDto>
    {
        private readonly GeneroRepositorio _repositorio;
        private readonly IMapper _map;
        public GeneroServico()
        {
        }
        public GeneroServico(DataContext contexto, IMapper mapper)
        {
            _repositorio = new GeneroRepositorio(contexto);
            _map = mapper;
        }

        public async Task<GeneroDto> Incluir(GeneroDto generoDto)
        {
            Genero genero = _map.Map<Genero>(generoDto);
            this.ValidarGenero(genero);
            return _map.Map<GeneroDto>(await _repositorio.Incluir(genero));
        }
        public async Task<GeneroDto> Alterar(GeneroDto generoDto)
        {
            Genero genero = _map.Map<Genero>(generoDto);
            this.ValidarGenero(genero);
            return _map.Map<GeneroDto>(await _repositorio.Alterar(genero));
        }
        public async Task<GeneroDto> Excluir(int id)
        {
            return _map.Map<GeneroDto>(await _repositorio.Excluir(id));
        }
        public async Task<List<GeneroDto>> Listar()
        {
            return _map.Map<List<GeneroDto>>(await _repositorio.Listar());
        }
        public async Task<GeneroDto> BuscarId(int id)
        {
            return _map.Map<GeneroDto>(await _repositorio.BuscarId(id)) ;
        }
        public bool Existe(int id)
        {
            return _repositorio.Existe(id);
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
    }
}
