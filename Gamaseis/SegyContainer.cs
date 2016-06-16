using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unplugged.Segy;

namespace Gamaseis
{
    class SegyContainer
    {
        public string FileName { get; set; }
        public ISegyFile Data { get; set; }
    }

    class ShotGather
    {
        public int[] Index;
        public IEnumerable<ITrace> Traces;
    }
}
