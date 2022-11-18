using System;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Servicos.Util;

namespace VideoTeca.Servicos.Validar
{
    public static class ClienteValidar
    {
        public static string ValidarCliente(Cliente cliente)
        {
            string _retorno = "";
            cliente.NumCep = cliente.NumCep.PadLeft(8, '0');
            cliente.NumDocumento = cliente.TipoPessoa == 1 ? cliente.NumDocumento.PadLeft(11, '0') : cliente.NumDocumento.PadLeft(14, '0');

            _retorno = cliente switch
            {
                _ when cliente.NomCliente == "" => _retorno = "Nome inválido.",
                _ when cliente.DesBairro == "" => _retorno = "Bairro inválido.",
                _ when cliente.DesMunicipio == "" => _retorno = "Município inválido.",
                _ when cliente.NumCep == "" => _retorno = "CEP inválido.",
                _ when cliente.NumEndereco == "" => _retorno = "Número do endereço inválido.",
                _ when cliente?.NumTelefone.Length < 10 || !cliente.NumTelefone.IsNumeric() => _retorno = "Telefone inválido.",
                _ when cliente.NumCep.Length != 8 || !cliente.NumCep.IsNumeric() => _retorno = "CEP inválido.",
                _ when !Util.Util.ValidarCpfCnpj(cliente.TipoPessoa, cliente.NumDocumento) => "CPF/CNPJ inválido.",
                _ when cliente.DtcNascimento == null || cliente.DtcNascimento >= DateTime.Now => "Data de nascimento inválida.",
                _ => ""
            };

            return _retorno;
        }
        public static bool ValidarPesquisa(FiltroClienteDto filtro)
        {
            bool _retorno = false;

            _retorno = filtro switch
            {
                _ when string.IsNullOrEmpty(filtro.Nome) && string.IsNullOrEmpty(filtro.NumDocumento) => _retorno = true,
                _ when !string.IsNullOrEmpty(filtro.NumDocumento) && !filtro.NumDocumento.IsNumeric() => _retorno = true,
                _ when filtro.CodTipoPessoa == null || !filtro.CodTipoPessoa.ToString().IsNumeric() => _retorno = true,

                _ => false
            };

            return _retorno;
        }
    }
}
