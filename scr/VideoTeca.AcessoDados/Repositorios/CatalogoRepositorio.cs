using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VideoTeca.AcessoDados.Contexto;
using VideoTeca.Modelos.Modelos;

namespace VideoTeca.AcessoDados.Repositorios
{
    public class CatalogoRepositorio : GenericoRepositorio<Catalogo>
    {
        protected readonly DataContext _contexto;
        public CatalogoRepositorio(DataContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }
        public override async Task<Catalogo> BuscarId(int id)
        {
            Catalogo catalogo = await _contexto.Catalogos
                                .AsNoTracking()
                                .Include(c => c.CatalogoTipoMidias).ThenInclude(CatalogoTipoMidias => CatalogoTipoMidias.TipoMidia)
                                .FirstOrDefaultAsync(c => c.Id == id);

            return catalogo;

            /*
            IQueryable<Property> query = DataContext.PropertyToLists.Where(x => x.Selected == true)
                                        .Where(x => x.UserId == userId && x.ListId == listId)
                                        // After identifying the relations that I need,
                                        // I only need to property object "which is a virtual property in" the relation object
                                        .Select(x => x.Property)
                                        // Here I am including relations from the Property virtual property which are virtual properties
                                        // on the Property
                                        .Include(x => x.City)
                                        .Include(x => x.Type)
                                        .Include(x => x.Status);

            List<Property> properties = await query.ToListAsync();
            */
        }
        public bool ExisteCodigo(string codCodigo)
        {
            bool retorno = _contexto.Catalogos.Any(e => e.Codigo == codCodigo);
            return retorno;
        }
        public bool ExisteCodigo(string codCodigo, int id)
        {
            bool retorno = _contexto.Catalogos.Any(e => e.Codigo == codCodigo && e.Id != id);
            return retorno;
        }
        public bool Existe(long id)
        {
            bool retorno = _contexto.Catalogos.Any(e => e.Id == id);
            return retorno;
        }
    }
}
