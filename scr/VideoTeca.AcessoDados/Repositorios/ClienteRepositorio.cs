using System;
using System.Collections.Generic;
using System.Linq;
using VideoTeca.Modelos.Modelos;
using VideoTeca.AcessoDados.Contexto;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class ClienteRepositorio: GenericoRepositorio<Cliente>
    {
        protected readonly DbSet<Cliente> dbSetClientes;
        protected readonly DataContext _contexto;
        public ClienteRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
            this.dbSetClientes = _context.Set<Cliente>();
        }
        public override async Task<List<Cliente>> Listar()
        {
            var entity = await dbSetClientes.AsNoTracking<Cliente>()
                .ToListAsync();
            //foreach(var item in entity)
            //{
            //    item.Catalogos = _contexto.Catalogos.FirstOrDefault(a => a.Id == item.Id);
            //    // item.FoneTipo = _contexto.FoneTipos.FirstOrDefault(a => a.Id == item.FoneTipoId);
            //}
            return entity;
        }
        public override async Task<Cliente> BuscarId(int id)
        {
            var entity = await dbSetClientes.AsNoTracking<Cliente>()
         //       .Include(a => a.Catalogos)
                .FirstOrDefaultAsync(a => a.Id == id);
            return entity;
        }
        public bool ExisteCpfCnpj(string numDocumento)
        {
            bool retorno = _contexto.Clientes.Any(e => e.NumDocumento == numDocumento);
            return retorno;
        }
        public bool ExisteCpfCnpj(string numDocumento, int id)
        {
            bool retorno = _contexto.Clientes.Any(e => e.NumDocumento == numDocumento && e.Id != id);
            return retorno;
        }
    }
 }
