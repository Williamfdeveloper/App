using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Domain.Contracts.Repository
{
    public  interface IProdutoRepository
    {
        IList<Produto> ListarProdutos();
        Produto BuscarProduto(int codigoProduto);
        bool SalvarProduto(Produto produto);
        bool AtualizarProduto(Produto produto);
        bool DeletarProduto(Produto produto);
    }
}
