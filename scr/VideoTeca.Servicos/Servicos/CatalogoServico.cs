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
using VideoTeca.Servicos.Validar;
using System;
using VideoTeca.Servicos.Util;

namespace VideoTeca.Servicos.Servicos
{ 
    public class CatalogoServico : IModeloCRUDInterface<Catalogo, CatalogoDto>
    {
        private readonly CatalogoRepositorio _repositorio;
        private readonly IMapper _map;
        private readonly DataContext _contexto;
        public CatalogoServico()
        {

        }      
        public CatalogoServico(DataContext contexto, IMapper mapper)
        {
            _repositorio = new CatalogoRepositorio(contexto);
            _map = mapper;
            _contexto = contexto;
        }

        public async Task<CatalogoDto> Incluir(Catalogo catalogo)
        {
            this.ValidarCatalogo(catalogo);
            if (_repositorio.ExisteCodigo(catalogo.Codigo))
            {
                throw new ArgumentException("Código já existe.");
            }
            CatalogoDto catalogoDto = new CatalogoDto();
            catalogoDto = _map.Map<CatalogoDto>(await _repositorio.Alterar(catalogo));

            return catalogoDto;
        }
        public async Task<CatalogoDto> Alterar(Catalogo catalogo)
        {
            CatalogoDto catalogoDto = new CatalogoDto();
            List<CatalogoTipoMidia> catalogoTipoMidia;
            this.ValidarCatalogo(catalogo);
            if (_repositorio.ExisteCodigo(catalogo.Codigo, catalogo.Id))
            {
                throw new ArgumentException("Código já existe.");
            }
            using (var transaction = _contexto.Database.BeginTransaction())
            {
                try
                {
                    catalogoTipoMidia = await new CatalogoTipoMidiaServico(_contexto).Pesquisar(catalogo.Id);
                    foreach (var item in catalogoTipoMidia)
                    {
                        if (! catalogo.CatalogoTipoMidias.Any(c => c.Id == item.Id))
                        {
                            await new CatalogoTipoMidiaServico(_contexto).Excluir(item.Id);
                        }
                    }
                    catalogoDto = _map.Map<CatalogoDto>(await _repositorio.Alterar(catalogo));
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    if (transaction.TransactionId != null)
                    {
                        await transaction.RollbackAsync();
                    }
                    throw new ArgumentException("Existe(m) títulos alugados. Impossivel exluir.");
                }

            }
            return catalogoDto;
        }
        public async Task<CatalogoDto> Excluir(int id)
        {
            if (id == 0)
            {
               throw new ArgumentException("Código inválido.");
            }
            return _map.Map<CatalogoDto>(await _repositorio.Excluir(id));
        }
        public async Task<List<CatalogoDto>> Listar()
        {
            List<Catalogo> catalogo = await _repositorio.Listar();
            List<CatalogoDto> catalogoDto = _map.Map<List<CatalogoDto>>(catalogo);
            buscarGenero(catalogoDto);
            return catalogoDto;
        }
        public async Task<Catalogo> BuscarId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Código inválido.");
            }
            Catalogo catalogo = await _repositorio.BuscarId(id);
            catalogo.CatalogoTipoMidiasDto = _map.Map<List<CatalogoTipoMidiaDto>>(catalogo.CatalogoTipoMidias);
            foreach (var item in catalogo.CatalogoTipoMidiasDto)
            {
                item.Descricao = new TipoMidiaServico(_contexto).BuscarId(item.IdTipoMidia)?.Result.Descricao;
            }
            return catalogo;
        }
        public async Task<List<CatalogoDto>> Pesquisar(FiltroCatalogoDto filtro)
        {
            this.ValidarPesquisa(filtro);

            Expression<Func<Catalogo, bool>> where = f => true;
            if (! string.IsNullOrEmpty(filtro.Titulo))
            {
                where = Util.ExpressionCombiner.And<Catalogo>(where, c => c.DesTitulo.Contains(filtro.Titulo));
            }
            if (! string.IsNullOrEmpty(filtro.IdGenero) && filtro.IdGenero != "0")
            {
                int idGenero = Convert.ToInt32(filtro.IdGenero);
                where = Util.ExpressionCombiner.And<Catalogo>(where, c => c.IdGenero == idGenero || idGenero == 0);
            }
            if (! string.IsNullOrEmpty(filtro.NomeAutor))
            {
                where = Util.ExpressionCombiner.And<Catalogo>(where, c => c.NomAutor.Contains(filtro.NomeAutor));
            }

            List<Catalogo> catalogo = await _repositorio.Listar(where);
            List<CatalogoDto> catalogoDto = _map.Map<List<CatalogoDto>>(catalogo);
            buscarGenero(catalogoDto);
            return catalogoDto;
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        private void ValidarCatalogo(Catalogo catalogo)
        {
            string retorno = CatalogoValidar.ValidarCatalogo(catalogo, _contexto);
            if (!string.IsNullOrEmpty(retorno))
            {
                throw new ArgumentException(retorno);
            }
        }
        public void ValidarPesquisa(FiltroCatalogoDto filtro)
        {
            if (CatalogoValidar.ValidarPesquisa(filtro))
            {
                throw new ArgumentException("Filtro de pesquisa inválido.");
            }
        }
        public Catalogo TesteCatalogo(Catalogo catalogo)
        {
            Catalogo retorno = new Catalogo();
            string validar = CatalogoValidar.ValidarCatalogo(catalogo, _contexto);
            if (string.IsNullOrEmpty(validar))
            {
                retorno = catalogo;
            }
            return retorno;
        }
        private void buscarGenero(List<CatalogoDto> catalogoDto)
        {
            GeneroServico _genero = new GeneroServico(_contexto);
            foreach (var item in catalogoDto)
            {
                item.DesGenero = _genero.BuscarId(item.IdGenero)?.Result.Descricao;
            }
        }

    }
}
