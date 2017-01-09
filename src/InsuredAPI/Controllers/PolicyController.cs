using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuredAPI.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace InsuredAPI.Controllers
{
    [Route("api/[controller]")]
    public class PolicyController : Controller
    {
        IPolicyRepository PolicyItems { get; set; }
        public PolicyController(IPolicyRepository policyItems)
        {
            PolicyItems = policyItems;
        }

        // GET: api/values
        [HttpGet]
        public List<Policy> GetPolicies()
        {
            return PolicyItems.GetPolicies();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
