using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuredAPI.Model
{
    public interface IPolicyRepository
    {
        List<Policy> GetPolicies();
    }
}
