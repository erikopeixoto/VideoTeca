using System;
using Xunit;
using VideoTeca.Modelos.Modelos;
using ExpectedObjects;
using VideoTeca.Servicos.Servicos;

namespace VideoTeca.Test.Cadastros
{
    public class GeneroTest
    {
        [Fact(DisplayName = "Cadastro de G�neros")]
        public void ValidarGenero()
        {
            GeneroServico _servico = new GeneroServico();
            Genero _generoNovo = new Genero();
            _generoNovo.Descricao = "A��o e Aventura";
            Genero _genero = _servico.TesteGenero(_generoNovo);
            _genero.ToExpectedObject().ShouldMatch(_generoNovo);
        }
    }
}
