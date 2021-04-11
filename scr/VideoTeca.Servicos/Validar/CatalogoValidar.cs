using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Util;
using VideoTeca.Modelos.Dtos;
using VideoTeca.Servicos.Servicos;
using VideoTeca.AcessoDados.Contexto;

namespace VideoTeca.Servicos.Validar
{
    public static class CatalogoValidar
    {
        public static string ValidarCatalogo(Catalogo catalogo, DataContext contexto)
        {
            string _retorno = "";
            GeneroServico _servico = new GeneroServico(contexto);
            TipoMidiaServico _tipoMidia = new TipoMidiaServico(contexto);
            catalogo.DtcAtualizacao = DateTime.Now;
            _retorno = catalogo switch
            {
                _ when string.IsNullOrEmpty(catalogo.DesTitulo) => _retorno = "Título inválido.",
                _ when string.IsNullOrEmpty(catalogo.NomAutor) => _retorno = "Autor/Diretor inválido.",
                _ when string.IsNullOrEmpty(catalogo.AnoLancamento) => _retorno = "Ano inválido.",
                _ when catalogo?.AnoLancamento.Length != 4 => _retorno = "Ano inválido.",
                _ when string.IsNullOrEmpty(catalogo.Codigo) => _retorno = "Código inválido.",
                _ when ! _servico.Existe(catalogo.IdGenero) => _retorno = "Gênero inválido.",
                _ => ""
            };
            
            if (catalogo.CatalogoTipoMidias != null && catalogo.CatalogoTipoMidias.Count > 0 && _retorno == "")
            {
                foreach (var item in catalogo.CatalogoTipoMidias)
                {
                    item.DtcAtualizacao = DateTime.Now;
                    _retorno = item switch
                    {
                        _ when ! _tipoMidia.Existe(item.TipoMidiaId) => _retorno = "Tipo mídia inválido.",
                        _ when item.QtdTitulo < 1 => _retorno = "Quantidade inválida.",
                        _ => ""
                    };
                }
            }

            return _retorno;
        }
        public static bool ValidarPesquisa(FiltroCatalogoDto filtro)
        {
            bool _retorno = false;

            _retorno = filtro switch
            {
                _ when string.IsNullOrEmpty(filtro.NomeAutor) && string.IsNullOrEmpty(filtro.Titulo) &&
                       ! filtro.IdGenero.IsNumeric() => _retorno = true,
                _ when ! string.IsNullOrEmpty(filtro.IdGenero) && !filtro.IdGenero.IsNumeric() => _retorno = true,
                _ => false
            };

            return _retorno;
        }
    }
}
