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
using VideoTeca.Servicos.Validar;

namespace VideoTeca.Servicos.Servicos
{
    public class CatalogoServico : ICatalogoInterface
    {
        private readonly CatalogoRepositorio _repositorio;
        private readonly IMapper _map;
        private readonly DataContext _contexto;
        private readonly CatalogoTipoMidiaServico _catalogoTipoMidiaServico;

        public CatalogoServico(DataContext contexto, IMapper mapper)
        {
            _repositorio = new CatalogoRepositorio(contexto);
            _map = mapper;
            _contexto = contexto;
            _catalogoTipoMidiaServico = new CatalogoTipoMidiaServico(_contexto, _map);
        }

        public async Task<CatalogoDto> Incluir(CatalogoDto catalogoDto)
        {
            Catalogo catalogo = _map.Map<Catalogo>(catalogoDto);
            catalogo.CatalogoTipoMidias = _map.Map<List<CatalogoTipoMidia>>(catalogoDto.CatalogoTipoMidiasDto);
            this.ValidarCatalogo(catalogo);
            if (_repositorio.ExisteCodigo(catalogo.Codigo))
            {
                throw new ArgumentException("Código já existe.");
            }
            catalogo?.CatalogoTipoMidias?.ForEach(c => c.QtdDisponivel = c.QtdTitulo);
            catalogoDto = _map.Map<CatalogoDto>(await _repositorio.Alterar(catalogo));

            return catalogoDto;
        }
        public async Task<CatalogoDto> Alterar(CatalogoDto catalogoDto)
        {
            List<CatalogoTipoMidia> catalogoTipoMidia;
            Catalogo catalogo = _map.Map<Catalogo>(catalogoDto);
            catalogo.CatalogoTipoMidias = _map.Map<List<CatalogoTipoMidia>>(catalogoDto.CatalogoTipoMidiasDto);
            this.ValidarCatalogo(catalogo);
            if (_repositorio.ExisteCodigo(catalogo.Codigo, catalogo.Id))
            {
                throw new ArgumentException("Código já existe.");
            }

            int diferenca;

            catalogo?.CatalogoTipoMidias?.ForEach(c =>
            {
                CatalogoTipoMidia catalogoTipoMidia = _catalogoTipoMidiaServico.BuscarId(c.Id).Result;
                if (catalogoTipoMidia != null)
                {
                    if (catalogoTipoMidia.QtdTitulo > c.QtdTitulo)
                    {
                        diferenca = (catalogoTipoMidia.QtdTitulo - c.QtdTitulo);
                        catalogoTipoMidia.QtdDisponivel = catalogoTipoMidia.QtdDisponivel + diferenca;
                    }
                    else if (catalogoTipoMidia.QtdTitulo < c.QtdTitulo)
                    {
                        diferenca = (c.QtdTitulo - catalogoTipoMidia.QtdTitulo);
                        catalogoTipoMidia.QtdDisponivel = catalogoTipoMidia.QtdDisponivel - diferenca;
                        if (catalogoTipoMidia.QtdDisponivel < 0)
                        {
                            throw new ArgumentException("Existem catálogos locados que impedem essa alteração.");
                        }
                    }
                }
                else
                {
                    c.QtdDisponivel = c.QtdTitulo;
                }
            });

            using (var transaction = _contexto.Database.BeginTransaction())
            {
                try
                {
                    catalogoTipoMidia = _catalogoTipoMidiaServico.Pesquisar(catalogo.Id).Result;
                    foreach (var item in catalogoTipoMidia)
                    {
                        if (!catalogo.CatalogoTipoMidias.Any(c => c.Id == item.Id))
                        {
                            await _catalogoTipoMidiaServico.Excluir(item.Id);
                        }
                    }

                    catalogoDto = _map.Map<CatalogoDto>(await _repositorio.Alterar(catalogo));
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    if (transaction?.TransactionId != null)
                    {
                        await transaction.RollbackAsync();
                    }
                    throw new ArgumentException("Existe(m) títulos alugados. Impossível exluir.");
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
        public async Task<CatalogoDto> BuscarId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Código inválido.");
            }
            Catalogo catalogo = await _repositorio.BuscarId(id);
            CatalogoDto catalogoMidiasDto = _map.Map<CatalogoDto>(catalogo);

            //catalogo.CatalogoTipoMidiasDto = _map.Map<List<CatalogoTipoMidiaDto>>(catalogo.CatalogoTipoMidias);

            foreach (var item in catalogoMidiasDto.CatalogoTipoMidiasDto)
            {
                item.Descricao = new TipoMidiaServico(_contexto, _map).BuscarId(item.IdTipoMidia)?.Result.Descricao;
            }
            catalogoMidiasDto.DesGenero = new GeneroServico(_contexto, _map).BuscarId(catalogoMidiasDto.IdGenero)?.Result?.Descricao;
            return catalogoMidiasDto;
        }
        public async Task<List<CatalogoDto>> Pesquisar(FiltroCatalogoDto filtro)
        {
            this.ValidarPesquisa(filtro);

            Expression<Func<Catalogo, bool>> where = f => true;
            if (!string.IsNullOrEmpty(filtro.Titulo))
            {
                where = Util.ExpressionCombiner.And<Catalogo>(where, c => c.DesTitulo.Contains(filtro.Titulo));
            }
            if (!string.IsNullOrEmpty(filtro.IdGenero) && filtro.IdGenero != "0")
            {
                int idGenero = Convert.ToInt32(filtro.IdGenero);
                where = Util.ExpressionCombiner.And<Catalogo>(where, c => c.IdGenero == idGenero || idGenero == 0);
            }
            if (!string.IsNullOrEmpty(filtro.NomeAutor))
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
            string retorno = CatalogoValidar.ValidarCatalogo(catalogo, _contexto, _map);

            if (!string.IsNullOrEmpty(retorno))
            {
                throw new ArgumentException(retorno);
            }
            catalogo?.CatalogoTipoMidias?.ForEach(c =>
            {
                if (c.Id != _catalogoTipoMidiaServico.ExisteCódigo(catalogo.Id, c.IdTipoMidia))
                {
                    throw new ArgumentException("Existe tipo de mídias duplicados.");
                }
            });
            var grupos = catalogo?.CatalogoTipoMidias?.GroupBy(c => new { c.IdCatalogo, c.IdTipoMidia }).Select(c => new { c.Key, Total = c.Count() }).ToList();
            //IEnumerable<object> quantidade1 = from a in catalogo.CatalogoTipoMidias
            //             group a by new { a.IdCatalogo, a.IdTipoMidia } into g
            //             select new { g.Key, Count = g.Count() };

            grupos?.ForEach(c =>
            {
                if (c.Total > 1)
                {
                    throw new ArgumentException("Existe tipo de mídias duplicados para o catalogo.");
                }
            });
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
            string validar = CatalogoValidar.ValidarCatalogo(catalogo, _contexto, _map );
            if (string.IsNullOrEmpty(validar))
            {
                retorno = catalogo;
            }
            return retorno;
        }
        public void buscarGenero(List<CatalogoDto> catalogoDto)
        {
            GeneroServico _genero = new GeneroServico(_contexto, _map);
            foreach (var item in catalogoDto)
            {
                item.DesGenero = _genero.BuscarId(item.IdGenero)?.Result.Descricao;
            }
        }

    }
}
