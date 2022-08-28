using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Authorize(Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        [HttpPost]
        [Route("AdicionarItemPedido")]
        public ActionResult AdicionarItemPedido([FromBody] string userId, string code)
        {
            return Ok();

        }
    }
}
