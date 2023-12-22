using e_commerce_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;
using e_commerce_API.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace e_commerce_API.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IWatchService _watchService;

        
        public ShoppingCartController(IWatchService watchService){
            _watchService = watchService;
        }

        [HttpPost(Name = "Checkout")]
        public JsonResult Checkout([FromBody] List<string> watches)
        {
            return _watchService.CheckoutWatches(watches);
        }

    }
}
