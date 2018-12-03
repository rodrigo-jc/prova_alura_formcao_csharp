using CasaDoCodigo.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> Save(string nome);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {

        }

        public async Task<Categoria> Save(string nome)
        {          
            var categoria = dbSet
                .Where(ip => ip.Nome == nome)
                .SingleOrDefault();

            if (categoria == null)
            {
                categoria = new Categoria(nome);
                dbSet.Add(categoria);
                await contexto.SaveChangesAsync();
            }

            return categoria;
        }
    }
}
