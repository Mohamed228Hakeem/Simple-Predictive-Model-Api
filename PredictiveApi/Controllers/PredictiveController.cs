using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using PredictiveApi.Interfaces;

namespace PredictiveApi.Controllers
{
    [ApiController]
    [Route("api/predict")]
   
    public class PredictiveController : ControllerBase
    
    {

    private readonly IPythonService _pythonScriptService;

    public PredictiveController(IPythonService pythonScriptService)
    {
        _pythonScriptService = pythonScriptService;
    }


        [HttpGet("{dayOfYear}")]
        public async Task<ActionResult> predictDemand(int dayOfYear)
        {

            try
        {
            string output = await _pythonScriptService.PredictDemandAsync(dayOfYear);
            

            return new JsonResult(new Dictionary<string, string>
            {
                { "Prediction", output }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }


        }

        
       
    }
}