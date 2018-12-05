using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto,
            ICategoriaRepository categoriaRepository
            ) : base(contexto)
        {
            this._categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {
            return await dbSet.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<IList<Produto>> GetProdutos(string pesquisa)
        {
            if (string.IsNullOrEmpty(pesquisa))
                return await GetProdutos();

            var busca = await dbSet.Include(p => p.Categoria)
                .Where(p => p.Nome.Contains(pesquisa) || p.Categoria.Nome.Contains(pesquisa)).ToListAsync();
            return busca;
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    Categoria categoria = await _categoriaRepository.Save(livro.Categoria);
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
