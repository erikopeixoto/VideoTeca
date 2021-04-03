using System;
using Xunit;
using VideoTeca.Modelos.Modelos;
using ExpectedObjects;
using VideoTeca.Servicos.Servicos;

namespace VideoTeca.Test.Cadastros
{
    public class ClienteTest
    {
        [Fact(DisplayName = "Cadastro de Clientes")]
        public void ValidarCliente()
        {
            ClienteServico _servico = new ClienteServico();
            Cliente _clienteNovo = new Cliente();
            _clienteNovo.NumDocumento = "34294678534";
            _clienteNovo.TipoPessoa = (byte) 1;
            _clienteNovo.NomCliente = "Antônio Érico da Silva Peixoto";
            _clienteNovo.NumTelefone = "7198599633";
            _clienteNovo.DesLogradouro = "Av. São Rafael";
            _clienteNovo.NumEndereco = "1025";
            _clienteNovo.NumCep = "41250410";
            _clienteNovo.DtcNascimento = DateTime.Parse("26/12/1956");
            Cliente _cliente = _servico.TesteCliente(_clienteNovo);

            _cliente.ToExpectedObject().ShouldMatch(_clienteNovo);
        }
    }
}
