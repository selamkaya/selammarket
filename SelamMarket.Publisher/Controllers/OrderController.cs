using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SelamMarket.Comman;
using SelamMarket.Publisher.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelamMarket.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private ISenderService _senderService;
        public OrderController(ISenderService senderService)
        {
            _senderService = senderService;
        }

        [HttpPost("/[controller]")]
        public async Task<ActionResult> Post(Order order)
        {
            return Ok(_senderService.SendOrder(order));
        }
    }
}
