using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
}
