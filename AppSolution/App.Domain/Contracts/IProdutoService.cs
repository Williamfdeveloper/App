using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Contracts
{
    public interface IProdutoService
    {
        IList<Produto> ListarProdutos();
        Produto BuscarProduto(int codigoProduto);
        bool SalvarProduto(Produto produto);
        bool AtualizarProduto(Produto produto);
        bool DeletarProduto(Produto produto);
    }
}
