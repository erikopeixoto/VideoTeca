using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VideoTeca.AcessoDados.Contexto;
using VideoTeca.AcessoDados.Repositorios;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Servicos.Interfaces;

namespace VideoTeca.Servicos.Servicos
{
    public class ClienteCatalogoTipoMidiaServico : IClienteCatalogoTipoMidiaInterface
    {
        private readonly IMapper _map;
        private readonly ClienteCatalogoTipoMidiaRepositorio _repositorio;
        private readonly DataContext _contexto;
        public ClienteCatalogoTipoMidiaServico()
        {
        }
        public ClienteCatalogoTipoMidiaServico(DataContext contexto, IMapper mapper)
        {
            _repositorio = new ClienteCatalogoTipoMidiaRepositorio(contexto);
            _map = mapper;
            _contexto = contexto;
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
        public async Task<List<ClienteCatalogoTipoMidia>> PesquisarCliente(int idCliente)
        {
            Expression<Func<ClienteCatalogoTipoMidia, bool>> where = f => true;
            if (idCliente != 0)
            {
                where = Util.ExpressionCombiner.And<ClienteCatalogoTipoMidia>(where, c => c.IdCliente == idCliente);
            }
            List<ClienteCatalogoTipoMidia> clienteCatalogoTipoMidia = await _repositorio.Listar(where);
            return clienteCatalogoTipoMidia;
        }
        public List<ClienteCatalogoTipoMidiaDto> Pesquisar(FiltroCatalogoDto filtro)
        {
            CatalogoServico catalogoServico = new CatalogoServico(_contexto, _map);

            catalogoServico.ValidarPesquisa(filtro);

            CatalogoTipoMidiaServico catalogoTipoMidiaServico = new CatalogoTipoMidiaServico(_contexto, _map);
            List<CatalogoTipoMidiaDto> catalogoTipoMidiaDtos;
            List<ClienteCatalogoTipoMidiaDto> clienteCatalogoTipoMidiaDtos = new List<ClienteCatalogoTipoMidiaDto>();

            List<CatalogoDto> catalogo = catalogoServico.Pesquisar(filtro)?.Result;

            Expression<Func<CatalogoTipoMidiaDto, bool>> whereMidia;
            foreach (var item in catalogo)
            {
                whereMidia = c => c.IdCatalogo == item.Id;
                catalogoTipoMidiaDtos = _map.Map<List<CatalogoTipoMidiaDto>>(catalogoTipoMidiaServico.Pesquisar(item.Id).Result.Where(c => c.QtdDisponivel > 0));
                catalogoTipoMidiaDtos?.ForEach(x =>
                   x.Descricao = new TipoMidiaServico(_contexto, _map).BuscarId(x.IdTipoMidia)?.Result?.Descricao
                );
                foreach (var itemMidia in catalogoTipoMidiaDtos)
                {
                    ClienteCatalogoTipoMidiaDto clienteCatalogoTipoMidiaDto = new ClienteCatalogoTipoMidiaDto();
                    clienteCatalogoTipoMidiaDto.DesTitulo = item.DesTitulo;
                    clienteCatalogoTipoMidiaDto.DesGenero = item.DesGenero;
                    clienteCatalogoTipoMidiaDto.DesTipoMidia = itemMidia.Descricao;
                    clienteCatalogoTipoMidiaDto.Codigo = item.Codigo;
                    clienteCatalogoTipoMidiaDto.AnoLancamento = item.AnoLancamento;
                    clienteCatalogoTipoMidiaDto.NomAutor = item.NomAutor;
                    clienteCatalogoTipoMidiaDto.QtdTitulo = itemMidia.QtdTitulo;
                    clienteCatalogoTipoMidiaDto.QtdDisponivel = itemMidia.QtdDisponivel;
                    clienteCatalogoTipoMidiaDtos.Add(clienteCatalogoTipoMidiaDto);
                }
            }
            return clienteCatalogoTipoMidiaDtos;
        }
        public async Task<ClienteCatalogoTipoMidia> BuscarId(int id)
        {
            return await _repositorio.BuscarId(id);
        }
        public bool Existe(int id)
        {
            return _repositorio.Existe(id);
        }
    }
}
