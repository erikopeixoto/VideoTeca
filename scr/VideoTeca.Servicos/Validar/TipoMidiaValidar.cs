using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Util;

namespace VideoTeca.Servicos.Validar
{
    public static class TipoMidiaValidar
    {
        public static string ValidarTipoMidia(TipoMidia tipoMidia)
        {
            string _retorno = "";
            tipoMidia.DtcAtualizacao = DateTime.Now;
            _retorno = tipoMidia switch
            {
                _ when tipoMidia.Descricao == "" => _retorno = "Descrição inválida.",
                _ => ""
            };

            return _retorno;
        }
    }
}
