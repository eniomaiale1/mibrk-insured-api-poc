using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuredAPI.Model
{
    public class PolicyRepository : IPolicyRepository
    {
        public PolicyRepository() {
            
        }

        public List<Policy> GetPolicies() {
            List<Policy> policies = new List<Policy>();
            policies.Add(new Policy { id = Guid.NewGuid().ToString(), policy_number = "policyNumber", effective = DateTime.Now, expiration = DateTime.Now.AddYears(1), agent = "Enio Maiale", type="Personal Auto", insured="Progresive" , image="auto.png", premium="1500" });
            policies.Add(new Policy { id = Guid.NewGuid().ToString(), policy_number = "policyNumber", effective = DateTime.Now, expiration = DateTime.Now.AddYears(1), agent = "Enio Maiale", type = "Personal Auto", insured = "Progresive", image = "auto.png", premium = "1500" });
            policies.Add(new Policy { id = Guid.NewGuid().ToString(), policy_number = "policyNumber", effective = DateTime.Now, expiration = DateTime.Now.AddYears(1), agent = "Enio Maiale", type = "Personal Auto", insured = "Progresive", image = "auto.png", premium = "1500" });
            return policies;
        }
    }
}
