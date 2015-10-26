using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using Unplugged.Segy;

namespace GamaseisProcessing
{
    public class Deconvolution
    {
        public static void WienerFilter(ISegyFile segy, float maxLag, float prewhite)
        {
            var nlag = (int) Math.Round((maxLag*1e6)/segy.Header.BinaryHeader.SampleIntervalReel);

            Debug.Write("Number of sample to be autocorrelated: ");
            Debug.WriteLine(nlag);

            WienerFilter(segy.Traces, nlag, prewhite);
            
        }

        
        public static void WienerFilter(IList<ITrace> traces , int nLag, float prewhite)
        {
            // Calculate the sum of autocorrelation of all traces.
            var sumautocorr = new float[nLag];

            // Sum of the first amplitude
            var w0 = 0f;

            foreach (var t in traces)
            {
                var autocorr = FFTApp.AutoCorr(t.Values.ToArray());
                w0 += t.Values[50];

                // take only from lag zero until to nLag
                for (var i = 0; i < nLag; i++)
                {
                    sumautocorr[i] += autocorr[i];
                }
            }

            Debug.WriteLine("Showing the 1000th amplitude of 10 traces");
            for (int i = 0; i < 10; i++)
            {
                Debug.WriteLine(traces[i].Values[32]);
            }

            Debug.WriteLine("Showing the sum of autocorrelation of traces");
            foreach (var f in sumautocorr)
            {
                Debug.WriteLine(f);
            }

            // the desired signal
            var desiredsignal = new float[nLag];
            desiredsignal[0] = Math.Abs(w0);

            Debug.WriteLine("Showing the desired signal");
            foreach (var f in desiredsignal)
            {
                Debug.WriteLine(f);
            }

            Debug.Write("The length of autocorrelation and desired signal: ");
            Debug.Write(sumautocorr.Length);
            Debug.Write(":");
            Debug.WriteLine(desiredsignal.Length);

            // find the optimum filter
            var opt = OptimumFilter(sumautocorr, desiredsignal, prewhite);
            WienerFilter(traces, opt);
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
            //Creating toeplitz matrix of autocorrelation
            var matbuilder = Matrix<float>.Build;
            var arrtoeplitz = CreateToeplitz(autocorr);
            var mattoeplitz = matbuilder.DenseOfArray(arrtoeplitz);


            //Adding whitenouse
            var pNoise = prewhite/100;
            mattoeplitz[0, 0] *= pNoise;
            mattoeplitz.Add(float.Epsilon);

            //Calculate the inverse filter
            var vecbuilder = Vector<float>.Build;
            var vecdesired = vecbuilder.DenseOfArray(desiredSignal);

            var opt = mattoeplitz.LU().Solve(vecdesired);

            return opt.ToArray();
        }

        public static void WienerFilter(IList<ITrace> traces, IList<float> invOperator)
        {
            foreach (var t in traces)
            {
                var ds = FFTApp.Conv(t.Values.ToArray(), invOperator.ToArray());
                for (var j = 0; j < t.Values.Count; j++)
                {
                    t.Values[j] = ds[j];
                }
            }
        }
    }
}