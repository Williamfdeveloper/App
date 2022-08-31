using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProdutoApiService _produtoApiService;

        public IndexModel(ILogger<IndexModel> logger, IProdutoApiService produtoApiService)
        {
            _produtoApiService = produtoApiService;
            _logger = logger;
        }

        public IList<Produto> Produtos { get; set; }

        public void OnGet()
        {
            Produtos = _produtoApiService.ListarProdutos();
        }
    }
}
