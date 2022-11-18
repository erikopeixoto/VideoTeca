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
    public class TipoMidiaServico : IModeloCRUDInterface<TipoMidiaDto>
    {
        private readonly TipoMidiaRepositorio repositorio;
        private readonly IMapper _map;
        public TipoMidiaServico()
        {
        }
        public TipoMidiaServico(DataContext contexto, IMapper mapper)
        {
            repositorio = new TipoMidiaRepositorio(contexto);
            _map = mapper;
        }

        public async Task<TipoMidiaDto> Incluir(TipoMidiaDto tipoMidiaDto)
        {
            TipoMidia tipoMidia = _map.Map<TipoMidia>(tipoMidiaDto);
            return _map.Map<TipoMidiaDto>(await repositorio.Incluir(tipoMidia));
        }
        public async Task<TipoMidiaDto> Alterar(TipoMidiaDto tipoMidiaDto)
        {
            TipoMidia tipoMidia = _map.Map<TipoMidia>(tipoMidiaDto);
            return _map.Map<TipoMidiaDto>(await repositorio.Alterar(tipoMidia));
        }
        public async Task<TipoMidiaDto> Excluir(int id)
        {
            return _map.Map<TipoMidiaDto>(await repositorio.Excluir(id));
        }
        public async Task<List<TipoMidiaDto>> Listar()
        {
            return _map.Map<List<TipoMidiaDto>>(await repositorio.Listar());
        }
        public async Task<TipoMidiaDto> BuscarId(int id)
        {
            return _map.Map<TipoMidiaDto>(await repositorio.BuscarId(id));
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
