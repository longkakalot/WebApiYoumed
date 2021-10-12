using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiNet5.Models;
using WebApiNet5.Repository;

namespace WebApiNet5.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ThongTinHcController : ControllerBase
    {
        private readonly ILogger<ThongTinHcController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IThongTinHanhChinhRepo _iThongTinHanhChinhRepo;


        public ThongTinHcController(ILogger<ThongTinHcController> logger, IWebHostEnvironment env
            , IThongTinHanhChinhRepo iThongTinHanhChinhRepo)
        {
            _logger = logger;
            _env = env;
            _iThongTinHanhChinhRepo = iThongTinHanhChinhRepo;
        }

        [Route("api/v1/nations")]
        [HttpGet]
        public async Task<IActionResult> GetDanTocs()
        {
            try
            {
                var kqAwait = await _iThongTinHanhChinhRepo.GetDanTocs();
                var kq = kqAwait.ToList();

                var nations = new Nation { Nations = kq.ToList() };
                Response.StatusCode = StatusCodes.Status200OK;
                Response.Headers.Add("Header", "Status code: " + Response.StatusCode.ToString());

                return new JsonResult(nations);                
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                Response.Headers.Add("Header", "Status code: " + Response.StatusCode.ToString());
                return new JsonResult("");
                 
            }            
        }

    }
}
