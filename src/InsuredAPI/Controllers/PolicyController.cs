using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InsuredAPI.Model;
using System.Net;
using System.Net.Http;

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

        [HttpGet, Route("{email}")]
        public JsonResult GetPolicies(string email)
        {
            List<Policy> result = null;
            try
            {
                result = PolicyItems.GetPolicies(email);
                return Json(result);
            }
            catch (ArgumentException es)
            {
                Response.StatusCode = 400;
                return Json(es);
            }
            catch (Exception ex) {
                Response.StatusCode = 500;
                return Json(ex);
            }
        }

        // GET api/attachments/{policyId}
        [HttpGet("attachments/{id}")]
        public JsonResult GetPolicyDocuments(string id)
        {
            List<InsuredAPI.Model.Attachment> result = null;
            try
            {
                result = PolicyItems.GetPolicyAttachment(id);
                return Json(result);
            }
            catch (ArgumentException es)
            {
                Response.StatusCode = 400;
                return Json(es);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex);
            }

        }

        // GET api/attachments/{documentName}
        [HttpGet("attachments/download/{policyId}/{documentName}")]
        public ActionResult GetFile(string policyId, string documentName)
        {
            string contentType = new InsuredAPI.Utility.Helper().GetMimeType(documentName);
            //new FileExtensionContentTypeProvider().TryGetContentType(documentName, out contentType);
            //contentType = contentType ?? "application/octet-stream";
            ControllerContext.HttpContext.Response.Headers.Add("Content-Disposition", String.Format("inline;filename=\"{0}\"", documentName));
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"C:\\Reports\\"+ policyId + "\\" + documentName);
            return File(fileBytes, contentType, documentName);

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
