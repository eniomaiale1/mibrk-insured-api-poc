using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuredAPI.Model
{
    public class Policy
    {
        public string id { get; set; }
        public string type { get; set; }
        public DateTime effective { get; set; }
        public DateTime expiration { get; set; }
        public string insured { get; set; }
        public string policy_number { get; set; }
        public string agent { get; set; }
        public string premium { get; set; }
        public string image { get; set; }

    }
}
