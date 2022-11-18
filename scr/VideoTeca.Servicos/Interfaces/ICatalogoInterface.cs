using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VideoTeca.Servicos.Dtos;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.Servicos.Interfaces
{
    public interface ICatalogoInterface
    {
        Task<CatalogoDto> Incluir(CatalogoDto obj);
        Task<List<CatalogoDto>> Listar();
        Task<CatalogoDto> Alterar(CatalogoDto obj);
        Task<CatalogoDto> Excluir(int obj);
        Task<CatalogoDto> BuscarId(int obj);
        Task<List<CatalogoDto>> Pesquisar(FiltroCatalogoDto obj);
    }
}
