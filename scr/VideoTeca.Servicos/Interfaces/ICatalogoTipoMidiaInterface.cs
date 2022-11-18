using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.Modelos.Modelos;
using VideoTeca.Servicos.Dtos;

namespace VideoTeca.Servicos.Interfaces
{
    public interface ICatalogoTipoMidiaInterface
    {
        Task<CatalogoTipoMidia> Incluir(CatalogoTipoMidia obj);
        Task<List<CatalogoTipoMidia>> Listar();
        Task<CatalogoTipoMidia> Alterar(CatalogoTipoMidia obj);
        Task<CatalogoTipoMidia> Excluir(int obj);
        Task<CatalogoTipoMidia> BuscarId(int obj);
        Task<List<CatalogoTipoMidia>> Pesquisar(int idCatalogo);

    }
}
