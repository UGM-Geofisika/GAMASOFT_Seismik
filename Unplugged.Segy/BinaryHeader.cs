namespace Unplugged.Segy
{
    internal class BinaryHeader : IBinaryHeader
    {
        public BinaryHeader(int jobNumber, int lineNumber, int reelNumber, int nDataTracesPerRecord,
            int nAuxTracesPerRecord, int sampleIntervalReel, int sampleIntervalField, int nDataPerTraceReel,
            int nDataPerTraceField, int sampleFormatCode, int cdpFold, int traceSortingCode, int verticalSumCode,
            int sweepFreqStart, int sweepFreqEnd, int sweepLength, int sweepTypeCode, int traceNumberOfSweepChannel,
            int sweepTraceTaperLengthStart, int sweepTraceTaperLengthEnd, int taperType, int correlatedDataTraces,
            int binaryGainRecovered, int amplitudeRecoveryMethod, int measurementSystem, int impulseSignal,
            int vibratoryPolarityCode)
        {
            JobNumber = jobNumber;
            LineNumber = lineNumber;
            ReelNumber = reelNumber;
            NDataTracesPerRecord = nDataTracesPerRecord;
            NAuxTracesPerRecord = nAuxTracesPerRecord;
            SampleIntervalReel = sampleIntervalReel;
            SampleIntervalField = sampleIntervalField;
            NDataPerTraceReel = nDataPerTraceReel;
            NDataPerTraceField = nDataPerTraceField;
            SampleFormatCode = sampleFormatCode;
            CdpFold = cdpFold;
            TraceSortingCode = traceSortingCode;
            VerticalSumCode = verticalSumCode;
            SweepFreqStart = sweepFreqStart;
            SweepFreqEnd = sweepFreqEnd;
            SweepLength = sweepLength;
            SweepTypeCode = sweepTypeCode;
            TraceNumberOfSweepChannel = traceNumberOfSweepChannel;
            SweepTraceTaperLengthStart = sweepTraceTaperLengthStart;
            SweepTraceTaperLengthEnd = sweepTraceTaperLengthEnd;
            TaperType = taperType;
            CorrelatedDataTraces = correlatedDataTraces;
            BinaryGainRecovered = binaryGainRecovered;
            AmplitudeRecoveryMethod = amplitudeRecoveryMethod;
            MeasurementSystem = measurementSystem;
            ImpulseSignal = impulseSignal;
            VibratoryPolarityCode = vibratoryPolarityCode;
        }

        public int JobNumber { get; private set; }
        public int LineNumber { get; private set; }
        public int ReelNumber { get; private set; }
        public int NDataTracesPerRecord { get; private set; }
        public int NAuxTracesPerRecord { get; private set; }
        public int SampleIntervalReel { get; private set; }
        public int SampleIntervalField { get; private set; }
        public int NDataPerTraceReel { get; private set; }
        public int NDataPerTraceField { get; private set; }
        public int SampleFormatCode { get; private set; }
        public int CdpFold { get; private set; }
        public int TraceSortingCode { get; private set; }
        public int VerticalSumCode { get; private set; }
        public int SweepFreqStart { get; private set; }
        public int SweepFreqEnd { get; private set; }
        public int SweepLength { get; private set; }
        public int SweepTypeCode { get; private set; }
        public int TraceNumberOfSweepChannel { get; private set; }
        public int SweepTraceTaperLengthStart { get; private set; }
        public int SweepTraceTaperLengthEnd { get; private set; }
        public int TaperType { get; private set; }
        public int CorrelatedDataTraces { get; private set; }
        public int BinaryGainRecovered { get; private set; }
        public int AmplitudeRecoveryMethod { get; private set; }
        public int MeasurementSystem { get; private set; }
        public int ImpulseSignal { get; private set; }
        public int VibratoryPolarityCode { get; private set; }
    }
}