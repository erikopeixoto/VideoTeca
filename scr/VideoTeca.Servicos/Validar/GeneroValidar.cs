using System;
using VideoTeca.Modelos.Modelos;

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
