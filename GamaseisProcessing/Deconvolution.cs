using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Unplugged.Segy;

namespace GamaseisProcessing
{
    internal class Deconvolution
    {
        public static ISegyFile WienerFilter(ISegyFile segy, float maxLag, float prewhite)
        {
            
            var nlag = (int) Math.Round((maxLag*1e6)/segy.Header.BinaryHeader.SampleIntervalReel);

            return WienerFilter(segy.Traces, nlag, prewhite);
        }

        public static ISegyFile WienerFilter(IList<ITrace> traces, int nLag, float prewhite)
        {
            // Calculate the sum of autocorrelation of all traces.
            var sumautocorr = new float[nLag];

            // Sum of the first amplitude
            var w0 = 0f;

            foreach (var t in traces)
            {
                var autocorr = FFTApp.AutoCorr(t.Values.ToArray());
                w0 += t.Values[0];

                // take only from lag zero until to nLag
                for (var i = 0; i < nLag; i++)
                {
                    sumautocorr[i] += autocorr[i];
                }
            }

            // the desired signal
            var desiredsignal = new float[nLag];
            desiredsignal[0] = Math.Abs(w0);

            // find the optimum filter

            var opt = OptimumFilter(sumautocorr, desiredsignal, prewhite);
            return WienerFilter(traces, opt);
        }

        /// <summary>
        /// Given a vector array v[n], return its symmetric toeplitz matrix.
        /// </summary>
        /// <param name="vectorarray"></param>
        /// <returns></returns>
        private static float[,] CreateToeplitz(IList<float> vectorarray)
        {
            var toeplitz = new float[vectorarray.Count, vectorarray.Count];

            for (var i = 0; i < vectorarray.Count; i++)
            {
                for (var j = 0; j < vectorarray.Count; j++)
                {
                    var index = Math.Abs(i - j);

                    toeplitz[i, j] = vectorarray[index];
                }
            }

            return toeplitz;
        }

        private static IList<float> OptimumFilter(IList<float> autocorr, float[] desiredSignal, float prewhite)
        {
            //TODO: Create toeplitz here
            var matbuilder = Matrix<float>.Build;
            var arrtoeplitz = CreateToeplitz(autocorr);
            var mattoeplitz = matbuilder.DenseOfArray(arrtoeplitz);


            //TODO: Add prewhitening here
            var pNoise = prewhite/100;
            mattoeplitz[0, 0] *= pNoise;
            mattoeplitz.Add(float.Epsilon);

            //TODO: Solve the filter
            var vecbuilder = Vector<float>.Build;
            var vecdesired = vecbuilder.DenseOfArray(desiredSignal);

            var opt = mattoeplitz.LU().Solve(vecdesired);

            return opt.ToArray();
        }

        public static ISegyFile WienerFilter(IList<ITrace> traces, IList<float> invOperator)
        {
            throw new NotImplementedException();
        }
    }
}