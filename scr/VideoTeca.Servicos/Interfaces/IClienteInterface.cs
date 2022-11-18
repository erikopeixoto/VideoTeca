using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;

namespace VideoTeca.Servicos.Interfaces
{
    public interface IClienteInterface
    {
        Task<ClienteDto> Incluir(Cliente obj);
        Task<List<ClienteDto>> Listar();
        Task<ClienteDto> Alterar(Cliente obj);
        Task<ClienteDto> Excluir(int obj);
        Task<Cliente> BuscarId(int obj);
        Task<List<ClienteDto>> Pesquisar(FiltroClienteDto filtro);
    }
}
