using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredictiveApi.Interfaces
{
    public interface IPythonService
    {
        Task<string> PredictDemandAsync(int dayOfYear);
    }
}