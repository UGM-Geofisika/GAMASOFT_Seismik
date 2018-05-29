using System;

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

        public static void Muting(ShotGather shot, float[,] coordinate, string muteType = "top")
        {
            var time = new float[coordinate.GetLength(1)+2];
            var offset = new float[coordinate.GetLength(1) + 2];
            time[0] = 0f;
            offset[0] = 0f;
            for (int i = 0; i < coordinate.GetLength(1); i++)
            {
                time[i+1] = coordinate[0, i];
                offset[i + 1] = coordinate[1, i];
            }
            time[time.Length - 1] = time[time.Length - 2];
            offset[offset.Length - 1] = shot.Traces[shot.Traces.Count-1].Header.Offset;

            var dt = shot.Traces[0].Header.Dt/ 1000000f;

            foreach (var trace in shot.Traces)
            {
                var traceOffset = trace.Header.Offset;
                var offsetIndex = new int();
                for (int j = 1; j < offset.Length; j++)
                {
                    if (traceOffset >= offset[j - 1] & traceOffset <= offset[j])
                    {
                        offsetIndex = j;
                        break;
                    }
                }
                var traceTime = LinearInterpolator(traceOffset, offset[offsetIndex - 1], time[offsetIndex - 1],
                    offset[offsetIndex], time[offsetIndex]);
                var indexTrace = (int) (traceTime/dt);
                if (muteType=="top")
                {
                    for (int i = 0; i < indexTrace; i++)
                    {
                        trace.Values[i] = 0f;
                    }
                }
            }
        }

        /// <summary>
        /// Calculate linear interpolation between two points in 2D space
        /// </summary>
        /// <param name="target">interpolation target</param>
        /// <param name="x1">x1 coordinate</param>
        /// <param name="y1">y1 coordinate</param>
        /// <param name="x2">x2 coordinate</param>
        /// <param name="y2">y2 coordinate</param>
        /// <param name="type">0 if target is in x coordinate and 1 if targer is in y coordinate</param>
        /// <returns></returns>
        private static float LinearInterpolator(float target, float x1, float y1, float x2, float y2, int type = 0)
        {
            // calculate gradient
            var gradient = (y2 - y1) / (x2 - x1);
            // calculate constant
            var constant = y1 - gradient * x1;
            // calculate interpolation
            if (type == 0)
            {
                return gradient * target + constant;
            }
            else
            {
                return (target - constant) / gradient;
            }
        }
    }
}
