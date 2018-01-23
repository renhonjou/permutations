using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using permutationsWeb.Models;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using permutationsWeb.Services;
using permutationsWeb.Helpers;
using permutationsWeb.Entities;

namespace permutationsWeb.Controllers
{
    [Route("api/[controller]")]
    public class PermutationsController : Controller
    {
        private readonly PermutationsRepository _permutationsRepository;

        public PermutationsController(PermutationsRepository permutationsRepository)
        {
            this._permutationsRepository = permutationsRepository;
        }

        // POST api/permutations
        [HttpPost]
        public JsonResult Post([FromBody] PermutationsRequest request)
        {
            if(request == null || request.Data == null || request.Data.Length == 0 || request.Data.Any(c => c.Length == 0 || c.Length > 8))
            {
                return Json(new { success = false, error = "args exception"});
            }
            
            var result = new List<PermutationsResponse>();
            foreach(var str in request.Data)
            {
                result.Add(this.GetPermutationsResponse(str));
            }
            
            return Json(new { success = true, data = result });
        }

        private PermutationsResponse GetPermutationsResponse(String request)
        {
            var cachedRequest = this._permutationsRepository.Get(request);
            if (cachedRequest != null)
            {
                return new PermutationsResponse
                {
                    Input = request,
                    Seconds = cachedRequest.Seconds,
                    Permutations = JsonHelper.Deserialize<String[]>(cachedRequest.PermutationsJson)
                };
            }

            var watch = Stopwatch.StartNew();
            var permutations = PermutationsService.GetPermutations(request);
            watch.Stop();

            this._permutationsRepository.Add(request, JsonHelper.Serialize(permutations), watch.ElapsedMilliseconds / 1000.0);
            return new PermutationsResponse
            {
                Input = request,
                Seconds = watch.ElapsedMilliseconds / 1000.0,
                Permutations = permutations
            };
            
        }
    }
}
