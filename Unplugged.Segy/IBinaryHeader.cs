namespace Unplugged.Segy
{
    public interface IBinaryHeader
    {
        /// <summary>
        ///     Job identification number.
        /// </summary>
        int JobNumber { get; }

        /// <summary>
        ///     Line number.
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        ///     Reel number.
        /// </summary>
        int ReelNumber { get; }

        /// <summary>
        ///     Number of data traces per record.
        /// </summary>
        int NDataTracesPerRecord { get; }

        /// <summary>
        ///     Number of auxiliary traces per record.
        /// </summary>
        int NAuxTracesPerRecord { get; }

        /// <summary>
        ///     Sample interval, microseconds, this file (reel).
        /// </summary>
        int SampleIntervalReel { get; }

        /// <summary>
        ///     Sample interval, microseconds, original field recording.
        /// </summary>
        int SampleIntervalField { get; }

        /// <summary>
        ///     Number of samples per data trace, this file (reel).
        /// </summary>
        int NDataPerTraceReel { get; }

        /// <summary>
        ///     Number of samples per data trace, original field recording.
        /// </summary>
        int NDataPerTraceField { get; }

        /// <summary>
        ///     Data sample format code:
        ///     1 = floating point (4 bytes)
        ///     2 = fixed point (4 bytes)
        ///     3 = fixed point (2 bytes)
        ///     4 = fixed point with gain code (4 bytes).
        /// </summary>
        int SampleFormatCode { get; }

        /// <summary>
        ///     CDP Fold
        /// </summary>
        int CdpFold { get; }

        /// <summary>
        ///     Trace sorting code:
        ///     1 = as recorded (no) sorting)
        ///     2 = CDP ensemble
        ///     3 = single fold continuous profile
        ///     4 = horizontally stacked
        /// </summary>
        int TraceSortingCode { get; }

        /// <summary>
        ///     Vertical sum code:
        ///     1 = no sum
        ///     2 = two sum ...
        ///     N = N sum (N = 32,767)
        /// </summary>
        int VerticalSumCode { get; }

        /// <summary>
        ///     Sweep frequency at start.
        /// </summary>
        int SweepFreqStart { get; }

        /// <summary>
        ///     Sweep frequency at end.
        /// </summary>
        int SweepFreqEnd { get; }

        /// <summary>
        ///     Sweep length, ms.
        /// </summary>
        int SweepLength { get; }

        /// <summary>
        ///     Sweep type code:
        ///     1 = linear
        ///     2 = parabolic
        ///     3 = exponential
        ///     4 = other
        /// </summary>
        int SweepTypeCode { get; }

        /// <summary>
        ///     Trace number of sweep channel.
        /// </summary>
        int TraceNumberOfSweepChannel { get; }

        /// <summary>
        ///     Sweep trace taper length, ms, at start if tapered.
        /// </summary>
        int SweepTraceTaperLengthStart { get; }

        /// <summary>
        ///     Sweep trace taper length, ms, at end.
        /// </summary>
        int SweepTraceTaperLengthEnd { get; }

        /// <summary>
        ///     Taper type: 1 = linear 2 = cos 3 = other
        /// </summary>
        int TaperType { get; }

        /// <summary>
        ///     Correlated data traces: 1 = no 2 = yes
        /// </summary>
        int CorrelatedDataTraces { get; }

        /// <summary>
        ///     Binary gain recovered: 1 = yes 2 = no
        /// </summary>
        int BinaryGainRecovered { get; }

        /// <summary>
        ///     Amplitude recovery method: 1 = none 2 = spherical divergence 3 = AGC 4 = other
        /// </summary>
        int AmplitudeRecoveryMethod { get; }

        /// <summary>
        ///     Measurement system: 1 = meters 2 = feet
        /// </summary>
        int MeasurementSystem { get; }

        /// <summary>
        ///     Impulse signal: 1 = Upward = negative number. 2 = Upward = positive number.
        /// </summary>
        int ImpulseSignal { get; }

        /// <summary>
        ///     Vibratory polarity code - seismic signal lags pilot signal by:
        ///     1 = 337.5 - 22.5 degrees
        ///     2 = 22.5 - 67.5 degrees
        ///     3 = 67.5 - 112.5 degrees
        ///     4 = 112.5 - 157.5 degrees
        ///     5 = 157.5 - 202.5 degrees
        ///     6 = 202.5 - 247.5 degrees
        /// </summary>
        int VibratoryPolarityCode { get; }
    }
}