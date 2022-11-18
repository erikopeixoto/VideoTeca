using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoTeca.Servicos.Interfaces
{
    public interface IModeloCRUDPInterface<T, K, Y> : IDisposable
    {
        Task<K> Incluir(T obj);
        Task<List<K>> Listar();
        Task<K> Alterar(T obj);
        Task<K> Excluir(int obj);
        Task<T> BuscarId(int obj);
        Task<List<K>> Pesquisar(Y obj);
    }
}
