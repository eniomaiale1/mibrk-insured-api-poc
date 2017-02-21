using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InsuredAPI.Data;

namespace InsuredAPI.Model
{
    public class PolicyRepository : IPolicyRepository
    {
        public PolicyRepository() {
            
        }

        public List<Policy> GetPolicies(string email) {
            if (string.IsNullOrEmpty(email))
            { 
                throw new ArgumentException("The email cannot be empty or null");
            }

            if (!new InsuredAPI.Utility.Helper().isValidEmail(email))
            {
                throw new ArgumentException("The email is not a valid email");
            }
            ZohoCRM zoho = new ZohoCRM();
            List<Policy> data = zoho.GetPolicies(email);
            return data;
        }

        public List<Model.Attachment> GetPolicyAttachment(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("The id cannot be empty or null");
            }

            ZohoCRM zoho = new ZohoCRM();
            List<Model.Attachment> data = zoho.GetPolicyAttachments(id);
            return data;
        }
    }
}
