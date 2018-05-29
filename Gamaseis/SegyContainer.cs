using System.Collections.Generic;
using Unplugged.Segy;

namespace Gamaseis
{
    public class SegyContainer
    {
        public string FileName { get; set; }
        public ISegyFile Data { get; set; }
    }

    public class ShotGather
    {
        public IList<ITrace> Traces;

        public void InitializeTraces(IEnumerable<ITrace> trace)
        {
            Traces = trace as List<ITrace>;
        }
    }
}
