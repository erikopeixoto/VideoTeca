using System;
using Xunit;
using VideoTeca.Modelos.Modelos;
using ExpectedObjects;
using VideoTeca.Servicos.Servicos;

namespace VideoTeca.Test.Cadastros
{
    public class TipoMidiaTest
    {
        [Fact(DisplayName = "Cadastro de G�neros")]
        public void ValidarTipoMidia()
        {
            TipoMidiaServico _servico = new TipoMidiaServico();
            TipoMidia _tipoMidiaNovo = new TipoMidia();
            _tipoMidiaNovo.Descricao = "A��o e Aventura";
            TipoMidia _tipoMidia = _servico.TesteTipoMidia(_tipoMidiaNovo);
            _tipoMidia.ToExpectedObject().ShouldMatch(_tipoMidiaNovo);
        }
    }
}
