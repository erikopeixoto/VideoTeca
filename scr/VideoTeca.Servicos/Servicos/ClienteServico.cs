using AutoMapper;
using System;
using System.Collections.Generic;
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
    public class ClienteServico : IClienteInterface
    {
        private readonly ClienteRepositorio _repositorio;
        private readonly IMapper _map;
        public ClienteServico(DataContext contexto, IMapper mapper)
        {
            _repositorio = new ClienteRepositorio(contexto);
            _map = mapper;
        }

        public async Task<ClienteDto> Incluir(Cliente cliente)
        {
            this.ValidarCliente(cliente);
            if (_repositorio.ExisteCpfCnpj(cliente.NumDocumento))
            {
                throw new ArgumentException("CPF/CNPJ existe.");
            }
            ClienteDto clienteDto = new ClienteDto();
            clienteDto = _map.Map<ClienteDto>(await _repositorio.Incluir(cliente));

            return clienteDto;
        }
        public async Task<ClienteDto> Alterar(Cliente cliente)
        {
            this.ValidarCliente(cliente);
            if (_repositorio.ExisteCpfCnpj(cliente.NumDocumento, cliente.Id))
            {
                throw new ArgumentException("CPF/CNPJ existe.");
            }
            return _map.Map<ClienteDto>(await _repositorio.Alterar(cliente));
        }
        public async Task<ClienteDto> Excluir(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Código inválido.");
            }
            return _map.Map<ClienteDto>(await _repositorio.Excluir(id));
        }
        public async Task<List<ClienteDto>> Listar()
        {
            List<Cliente> cliente = await _repositorio.Listar();
            List<ClienteDto> clienteDto = _map.Map<List<ClienteDto>>(cliente);
            return clienteDto;
        }
        public async Task<Cliente> BuscarId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("Código inválido.");
            }
            Cliente cliente = _map.Map<Cliente>(await _repositorio.BuscarId(id));
            return cliente;
        }
        public async Task<List<ClienteDto>> Pesquisar(FiltroClienteDto filtro)
        {
            this.ValidarPesquisa(filtro);

            Expression<Func<Cliente, bool>> where = f => true;
            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                where = Util.ExpressionCombiner.And<Cliente>(where, c => c.NomCliente.Contains(filtro.Nome));
            }
            if (!string.IsNullOrEmpty(filtro.CodTipoPessoa?.ToString()))
            {
                byte codTipo = Convert.ToByte(filtro.CodTipoPessoa);
                where = Util.ExpressionCombiner.And<Cliente>(where, c => c.TipoPessoa == filtro.CodTipoPessoa || filtro.CodTipoPessoa == 0);
            }
            if (!string.IsNullOrEmpty(filtro.NumDocumento))
            {
                filtro.NumDocumento = filtro.NumDocumento.PadLeft(filtro.CodTipoPessoa == 1 ? 11 : 14, '0');
                where = Util.ExpressionCombiner.And<Cliente>(where, c => c.NumDocumento == filtro.NumDocumento);
            }

            List<Cliente> cliente = await _repositorio.Listar(where);
            List<ClienteDto> clienteDto = _map.Map<List<ClienteDto>>(cliente);
            return clienteDto;
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
        private void ValidarCliente(Cliente cliente)
        {
            string retorno = ClienteValidar.ValidarCliente(cliente);
            if (!string.IsNullOrEmpty(retorno))
            {
                throw new ArgumentException(retorno);
            }
        }
        public void ValidarPesquisa(FiltroClienteDto filtro)
        {
            if (ClienteValidar.ValidarPesquisa(filtro))
            {
                throw new ArgumentException("Filtro de pesquisa inválido.");
            }
        }
        public Cliente TesteCliente(Cliente cliente)
        {
            Cliente retorno = new Cliente();
            string validar = ClienteValidar.ValidarCliente(cliente);
            if (string.IsNullOrEmpty(validar))
            {
                retorno = cliente;
            }
            return retorno;
        }

    }
}
