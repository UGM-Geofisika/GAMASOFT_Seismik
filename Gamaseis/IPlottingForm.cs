using System.Collections.Generic;

namespace Gamaseis
{
    public interface IPLottingForm
    {
        IList<ShotGather> Shots { get;}
        void Plot();
        void Update(IEnumerable<ShotGather> shots);
    }
}
