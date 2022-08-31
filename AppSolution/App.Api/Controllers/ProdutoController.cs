using App.Domain.Contracts;
using App.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace App.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_produtoService.ListarProdutos());
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Route("GetProduto")]
        public ActionResult GetProduto(int codigoProduto)
        {
            try
            {
                return Ok(_produtoService.BuscarProduto(codigoProduto));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
