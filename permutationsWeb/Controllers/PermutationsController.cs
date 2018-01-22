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
                var cachedRequest = this._permutationsRepository.Get(str);
                if(cachedRequest != null)
                {
                    var response = new PermutationsResponse
                    {
                        Input = str,
                        Seconds = cachedRequest.Seconds,
                        Permutations = JsonHelper.Deserialize<String[]>(cachedRequest.PermutationsJson)
                    };
                    result.Add(response);
                    continue;
                }

                var watch = Stopwatch.StartNew();
                var permutations = PermutationsService.GetPermutations(str);
                watch.Stop();

                result.Add(new PermutationsResponse {
                    Input = str,
                    Seconds = watch.ElapsedMilliseconds / 1000.0,
                    Permutations = permutations
                });
                this._permutationsRepository.Add(str, JsonHelper.Serialize(permutations), watch.ElapsedMilliseconds / 1000.0);
            }
            
            return Json(new { success = true, data = result });
        }
    }
}
