using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuredAPI.Data.Account
{
    public class FL
    {
        public string val { get; set; }
        public string content { get; set; }
    }

    public class Row
    {
        public string no { get; set; }
        public List<FL> FL { get; set; }
    }

    public class Accounts
    {
        public List<Row> row { get; set; }
    }

    public class Result
    {
        public Accounts Accounts { get; set; }
    }

    public class Response
    {
        public Result result { get; set; }
        public string uri { get; set; }
    }

    public class ZohoAccounts
    {
        public Response response { get; set; }
    }
}
