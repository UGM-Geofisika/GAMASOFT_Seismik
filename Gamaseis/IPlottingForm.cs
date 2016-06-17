using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gamaseis
{
    public interface IPLottingForm
    {
        IList<ShotGather> Shots { get;}
        void Plot();
        void Update(IEnumerable<ShotGather> shots);
    }
}
