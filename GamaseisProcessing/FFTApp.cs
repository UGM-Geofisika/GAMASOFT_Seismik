using System;
using System.Collections.Generic;
using Exocortex.DSP;

namespace GamaseisProcessing
{
    internal class FFTApp
    {
        /// <summary>
        ///     Given an integer v, return next closest power of 2.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static uint NextPowerOf2(uint v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;
            return v << 1;
        }

        /// <summary>
        ///     Given two signal, signalA[m] and signalB[n], return the convoluted signal of length m+n-1.
        /// </summary>
        /// <param name="signalA"></param>
        /// <param name="signalB"></param>
        /// <returns></returns>
        public static float[] Conv(float[] signalA, float[] signalB)
        {
            var result = new float[signalA.Length + signalB.Length - 1];
            var nfft = NextPowerOf2((uint) result.Length);
            var sa = AppendZeroPad(signalA, (uint) (result.Length - signalA.Length));
            var sb = AppendZeroPad(signalB, (uint) (result.Length - signalB.Length));

            sa = AppendZeroPad(sa, (uint) (nfft - sa.Length));
            sb = AppendZeroPad(sb, (uint) (nfft - sb.Length));

            Fourier.RFFT(sa, FourierDirection.Forward);
            Fourier.RFFT(sb, FourierDirection.Forward);

            var n2 = nfft/2;
            for (var i = 2; i < sa.Length; i += 2)
            {
                var c1 = new ComplexF(sa[i], sa[i + 1]);
                var c2 = new ComplexF(sb[i], sb[i + 1]);
                var c3 = c1*c2;

                sa[i] = c3.Re/n2;
                sa[i + 1] = c3.Im/n2;
            }

            sa[0] = (sa[0]*sb[0])/n2;
            sa[1] = (sa[1]*sb[1])/n2;

            Fourier.RFFT(sa, FourierDirection.Backward);
            Array.Copy(sa, result, result.Length);

            return result;
        }

        /// <summary>
        ///     Given a signal, return the autocorrelation at zero and increasingly positive lag.
        ///     Autocorrelation is calculated by FFT-ing the signal, multiply the resulting transform with its conjugate and
        ///     inverse transform the product.
        /// </summary>
        /// <param name="signal"></param>
        /// <returns></returns>
        public static float[] AutoCorr(float[] signal)
        {
            var result = new float[signal.Length];
            var nfft = NextPowerOf2((uint) signal.Length);
            var datacopy = AppendZeroPad(signal, nfft - (uint) signal.Length);

            Fourier.RFFT(datacopy, FourierDirection.Forward);
            var n2 = nfft/2;

            for (var i = 2; i < nfft; i += 2)
            {
                var c = new ComplexF(datacopy[i], datacopy[i + 1]);
                var cr = c*c.GetConjugate();
                datacopy[i] = cr.Re/n2;
                datacopy[i + 1] = cr.Im/n2;
            }

            datacopy[0] = (datacopy[0]*datacopy[0])/n2;
            datacopy[1] = (datacopy[1]*datacopy[1])/n2;

            Fourier.RFFT(datacopy, FourierDirection.Backward);
            Array.Copy(datacopy, result, result.Length);

            return result;
        }

        /// <summary>
        ///     Append the end of signal by n zero pads.
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static float[] AppendZeroPad(float[] signal, uint n)
        {
            var result = new float[signal.Length + n];
            Array.Copy(signal, result, signal.Length);
            return result;
        }
    }
}