using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CasaDoCodigo.Models.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel(IList<Produto> produtos, string pesquisa)
        {
            Produtos = produtos;
            Pesquisa = pesquisa;
        }

        public IList<Produto> Produtos { get; }

        [MinLength(5, ErrorMessage = "Nome deve ter no mínimo 5 caracteres")]
        public string Pesquisa;
    }
}
