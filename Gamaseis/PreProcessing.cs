using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamaseis
{
    public static class PreProcessing
    {
        public static void TimePowerCorrection(ShotGather shot, float pow)
        {
            foreach (var trace in shot.Traces)
            {
                var dt = trace.Header.Dt / 10e6f;
                for (var i = 0; i < trace.Values.Count; i++)
                {
                    trace.Values[i] = Time_Power(trace.Values[i], pow, dt, i);
                }
            }
        }
        public static void ExpGainCorrection(ShotGather shot, float pow)
        {
            foreach (var trace in shot.Traces)
            {
                var dt = trace.Header.Dt/10e6f;
                for (var i = 0; i < trace.Values.Count; i++)
                {
                    trace.Values[i] = Exp_Gain(trace.Values[i], pow, dt, i);
                }
            }
        }

        private static float Exp_Gain(float val, float pow, float dt, int i)
        {
            var t = dt*i;
            var result = val*Math.Exp(pow*t);
            return (float) result;
        }

        private static float Time_Power(float val, float pow, float dt, int i)
        {
            var t = dt * i;
            var result = val * Math.Pow(t,pow);
            return (float)result;
        }
    }
}
