using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoTeca.Servicos.Interfaces
{
    public interface IModeloCRUDInterface<T>
    {
        Task<T> Incluir(T obj);
        Task<List<T>> Listar();
        Task<T> Alterar(T obj);
        Task<T> Excluir(int obj);
        Task<T> BuscarId(int obj);
    }
}
