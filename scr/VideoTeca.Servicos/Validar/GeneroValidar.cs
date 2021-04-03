using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Util;

namespace VideoTeca.Servicos.Validar
{
    public static class GeneroValidar
    {
        public static string ValidarGenero(Genero genero)
        {
            string _retorno = "";
            genero.DtcAtualizacao = DateTime.Now;
            _retorno = genero switch
            {
                _ when genero.Descricao == "" => _retorno = "Descrição inválida.",
                _ => ""
            };

            return _retorno;
        }
    }
}
