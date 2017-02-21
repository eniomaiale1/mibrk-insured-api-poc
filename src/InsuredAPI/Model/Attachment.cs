using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuredAPI.Model
{
    public class Attachment
    {
        public string id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public DateTime modified { get; set; }
        public string modifiedBy { get; set; }
    }
}
