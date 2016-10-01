using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ILNumerics_Licensing;
using Unplugged.Segy;

namespace Gamaseis
{
    public static class VelocityAnalysis
    {
        /// <summary>
        /// Method to calculate velocity analysis
        /// </summary>
        /// <param name="dataGather">Data Gather</param>
        /// <param name="minVelocity">Minimum Velocity to analyze (m/s)</param>
        /// <param name="maxVelocity">Maximum Velocity to analyze (m/s)</param>
        /// <param name="velocityStep">Velocity step (m/s)</param>
        /// <param name="gateWidth">Gate Width in ms (microsecond)</param>
        /// <returns></returns>
        public static float[,] PerformVelocityAnalysis(ShotGather dataGather, float minVelocity, float maxVelocity,
            float velocityStep, float gateWidth)
        {
            var dt = dataGather.Traces[0].Header.Dt/1000000f;
            var gateLength = (int) (gateWidth / (dt*1000f));
            if (gateLength%2!=0)
            {
                gateLength += 1;
            }
            if (gateLength<=0) throw new Exception("Gate mustbe greater than 0!");
            var halfGate = gateLength/2;
            var dx = dataGather.Traces[1].Header.Offset - dataGather.Traces[0].Header.Offset;
            var numberOfStep = (int) ((maxVelocity - minVelocity)/velocityStep);
            var traceLength = dataGather.Traces[0].Values.Count;
            var velan = new float[numberOfStep, dataGather.Traces[0].Values.Count];
            for (int i = 0; i < numberOfStep; i++)
            {
                var velocity = minVelocity + (velocityStep*i);
                for (int j = 0; j < dataGather.Traces[0].Values.Count; j++)
                {
                    var timeLocation = new int[dataGather.Traces.Count];
                    var minId = new int[dataGather.Traces.Count];
                    var maxId = new int[dataGather.Traces.Count];
                    for (int k = 0; k < dataGather.Traces.Count; k++)
                    {
                        var time = ComputeTravelTime(velocity, dt * j, dataGather.Traces[k].Header.Offset);
                        timeLocation[k] = (int)(time / dt);
                        FindMinMaxId(traceLength, timeLocation[k], halfGate, out minId[k], out maxId[k]);
                    }
                    var stackEnergy = new float[gateLength];
                    var prestackEnergy = new float[gateLength];
                    var tmpVelan = 0f;
                    var isLessThanGate = false;
                    for (int k = 0; k < dataGather.Traces.Count; k++)
                    {
                        for (int l = minId[k]; l < maxId[k]; l++)
                        {
                            if (l < 0 | l >= traceLength)
                            {
                                isLessThanGate = true;
                                continue;
                            }
                            stackEnergy[l-minId[k]] += dataGather.Traces[k].Values[l];
                            prestackEnergy[l-minId[k]] += (float)Math.Pow(dataGather.Traces[k].Values[l], 2);
                        }
                        if (isLessThanGate) break;
                        tmpVelan = (1f / gateLength) * ((float)Math.Pow(stackEnergy.Sum(),2) / prestackEnergy.Sum());
                    }
                    if (isLessThanGate)
                    {
                        velan[i, j] = 0f;
                        continue;
                    }
                    //if (tmpVelan > 1f)
                    //{
                    //    velan[i, j] = 1f;
                    //}
                    //else
                    //{
                    //    velan[i, j] = tmpVelan;
                    //}
                    velan[i, j] = tmpVelan;
                }
            }
            return velan;
        }

        private static float ComputeTravelTime(float velocity, float originTime,float distance )
        {
            return (float) Math.Sqrt(Math.Pow(originTime, 2) + Math.Pow(distance/velocity, 2));
        }

        private static int FindMinimumIndex(IReadOnlyList<float> input)
        {
            var id = 0;
            var minValue = float.MaxValue;
            for (var i = 0; i < input.Count; i++)
            {
                if (!(minValue > input[i])) continue;
                id = i;
                minValue = input[i];
            }
            return id;
        }

        private static int FindMaximumIndex(IReadOnlyList<float> input)
        {
            var id = 0;
            var maxValue = float.MaxValue;
            for (int i = 0; i < input.Count; i++)
            {
                if (!(maxValue < input[i])) continue;
                id = i;
                maxValue = input[i];
            }
            return id;
        }

        private static void FindMinMaxId(int traceLength, int timeLocation, int halfGate, out int minId, out int maxId)
        {
            minId = timeLocation - halfGate;
            maxId = timeLocation + halfGate - 1;
        }
    }

    public static class NormalMoveOut
    {
        /// <summary>
        /// Perform Normal Move Out calculation to correct different offset in shot gather
        /// </summary>
        /// <param name="dataGather"></param>
        /// <param name="velanPick">2 x N array derived from </param>
        /// <param name="nmoStrechingLimit"></param>
        /// <returns></returns>
        public static ShotGather PerformNormalMoveOut(ShotGather dataGather, float[,] velanPick,
            float nmoStrechingLimit = 1f)
        {
            // check velan dimension
            if (velanPick.GetLength(0)!=2)
            {
                throw new Exception("First dimension od velanPick must be 2!");
            }
            
            var outputGather = dataGather;
            var dt = dataGather.Traces[0].Header.Dt / 1000000f;
            var dx = dataGather.Traces[1].Header.Offset - dataGather.Traces[0].Header.Offset;
            var npts = dataGather.Traces[0].Values.Count;
            var totalTime = dt*(npts-1);

            // create new t0 and velocity array with head and tail data addition
            var t0 = new float[velanPick.GetLength(1)+2];
            var v = new float[velanPick.GetLength(1) + 2];
            for (int i = 0; i < velanPick.GetLength(1); i++)
            {
                t0[i + 1] = velanPick[0, i];
                v[i + 1] = velanPick[1, i];
            }
            t0[0] = 0f;
            v[0] = v[1];
            t0[t0.Length-1] = totalTime;
            v[v.Length - 1] = v[v.Length - 2];

            for (int i = 0; i < dataGather.Traces.Count; i++)
            {
                if (i==0) continue;
                var tx = new float[npts];
                var dtNmo = new float[npts];
                var newPosition = new int[npts];
                var tNmo = new float[npts];
                for (var j = 0; j < tx.Length; j++)
                {
                    var timeIndex = new int();
                    for (int k = 1; k < t0.Length; k++)
                    {
                        if (dt*j>=t0[k-1] & dt * j < t0[k])
                        {
                            timeIndex = k;
                            break;
                        }
                    }
                    var velocity = LinearInterpolator(dt*j, t0[timeIndex - 1], v[timeIndex - 1], t0[timeIndex],
                        v[timeIndex]);
                    tx[j] = ComputeTravelTime(velocity,dt*j,dataGather.Traces[i].Header.Offset);
                    var tmp = (int) (tx[j]/dt);
                    if (tmp >= npts) break;
                    var strechingFactor = ComputeStrechingFactor(tx[j]/tmp, dt*j);
                    if (strechingFactor>nmoStrechingLimit) continue;
                    newPosition[(int) (tx[j]/dt)] = j;
                }
                // find non zero index
                var nonZeroIndex = FindFirstNonZero(newPosition);
                // normal move out correction
                for (int j = 0; j < newPosition[nonZeroIndex]; j++)
                {
                    outputGather.Traces[i].Values[j] = 0f;
                }
                for (int j = nonZeroIndex+1; j < npts; j++)
                {
                    if (newPosition[j]-newPosition[j-1]>1)
                    {
                        for (var k = newPosition[j-1]; k < newPosition[j]; k++)
                        {
                            outputGather.Traces[i].Values[k] = LinearInterpolator(k, newPosition[j - 1],
                                dataGather.Traces[i].Values[j - 1], newPosition[j], dataGather.Traces[i].Values[j]);
                        }
                    }
                    else
                    {
                        var k = newPosition[j - 1];
                        outputGather.Traces[i].Values[k] = dataGather.Traces[i].Values[j - 1];
                    }
                }
                if (newPosition[npts-1]<npts-1)
                {
                    for (int j = newPosition[npts-1]; j < npts; j++)
                    {
                        outputGather.Traces[i].Values[j] = 0f;
                    }
                }
            }
            return outputGather;
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
            var gradient = (y2 - y1)/(x2 - x1);
            // calculate constant
            var constant = y1 - gradient*x1;
            // calculate interpolation
            if (type==0)
            {
                return gradient*target+constant;
            }
            else
            {
                return (target - constant)/gradient;
            }
        }
        /// <summary>
        /// Calculate parabolic travel time on seismic reflection
        /// </summary>
        /// <param name="velocity">seismic velocity</param>
        /// <param name="originTime">t0</param>
        /// <param name="distance">offset from shotpoint</param>
        /// <returns></returns>
        private static float ComputeTravelTime(float velocity, float originTime, float distance)
        {
            return (float)Math.Sqrt(Math.Pow(originTime, 2) + Math.Pow(distance / velocity, 2));
        }

        /// <summary>
        /// Calculate streching factor for NMO analysis
        /// </summary>
        /// <param name="deltaTnmo"></param>
        /// <param name="t0"></param>
        /// <returns></returns>
        private static float ComputeStrechingFactor(float deltaTnmo, float t0)
        {
            return deltaTnmo/t0;
        }

        /// <summary>
        /// Mute selected index on single trace signal
        /// </summary>
        /// <param name="trace">Trace array</param>
        /// <param name="firstIndex">first muted index</param>
        /// <param name="lastIndex">last muted index</param>
        /// <returns></returns>
        private static float[] TraceMute(float[] trace, int firstIndex = 0, int lastIndex = 0)
        {
            for (var i = 0; i < trace.Length; i++)
            {
                if (lastIndex <= firstIndex) continue;
                if (i>=firstIndex & i<=lastIndex)
                {
                    trace[i] = 0f;
                }
            }
            return trace;
        }

        private static int FindFirstNonZero(int[] index)
        {
            var outputIndex = new int();
            for (var i = 0; i < index.Length; i++)
            {
                if (index[i]==0) continue;
                outputIndex = i;
                break;
            }
            return outputIndex;
        }
    }

    public static class Stacking
    {
        public static ShotGather PerformStack(ShotGather dataGather, string method = "average")
        {
            var npts = dataGather.Traces[0].Values.Count;
            var ntrace = dataGather.Traces.Count;
            var outputGather = new ShotGather();
            outputGather.InitializeTraces(new List<ITrace>(new ITrace[npts]));
            outputGather.Traces[0] = dataGather.Traces[0];

            for (int i = 0; i < npts; i++)
            {
                outputGather.Traces[0].Values[i] = 0f;
                for (int j = 0; j < ntrace; j++)
                {
                    outputGather.Traces[0].Values[i] += dataGather.Traces[j].Values[i]/ntrace;
                }
            }

            //offset correction
            return outputGather;
        }
    }
}
