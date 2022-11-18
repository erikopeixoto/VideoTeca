using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;

namespace VideoTeca.Servicos.Interfaces
{
    public interface IClienteCatalogoTipoMidiaInterface
    {
        Task<ClienteCatalogoTipoMidia> Incluir(ClienteCatalogoTipoMidia obj);
        Task<List<ClienteCatalogoTipoMidia>> Listar();
        Task<ClienteCatalogoTipoMidia> Alterar(ClienteCatalogoTipoMidia obj);
        Task<ClienteCatalogoTipoMidia> Excluir(int obj);
        Task<ClienteCatalogoTipoMidia> BuscarId(int obj);
        List<ClienteCatalogoTipoMidiaDto> Pesquisar(FiltroCatalogoDto obj);
        Task<List<ClienteCatalogoTipoMidia>> PesquisarCliente(int filtro);

    }
}
