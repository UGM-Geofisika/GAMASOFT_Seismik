using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unplugged.IbmBits;

namespace Unplugged.Segy
{
    /// <summary>
    ///     Responsible for reading SEGY files given a path or a Stream.
    /// </summary>
    public class SegyReader
    {
        public SegyReader()
        {
            Options = new SegyOptions();
            InlineNumberLocation = 189;
            CrosslineNumberLocation = 193;
        }

        public ISegyOptions Options { get; set; }
        public int InlineNumberLocation { get; set; }
        public int CrosslineNumberLocation { get; set; }

        #region From the Top: Methods that start reading from the beginning of the file

        /// <summary>
        ///     Given a file path, reads entire SEGY file into memory
        /// </summary>
        public virtual ISegyFile Read(string path, IReadingProgress progress = null)
        {
            using (var stream = File.OpenRead(path))
                return Read(stream, progress);
        }

        /// <summary>
        ///     Given stream, reads entire SEGY file into memory.
        ///     Assumes the stream is at the start of the file.
        /// </summary>
        public virtual ISegyFile Read(Stream stream, IReadingProgress progress = null)
        {
            return Read(stream, int.MaxValue, progress);
        }

        /// <summary>
        ///     Given stream and traceCount, reads the requested number
        ///     of traces into memory. The given traceCount may exceed
        ///     the number of traces in the file;
        ///     in that case all the traces in the file are read.
        ///     Assumes the stream is at the start of the file.
        /// </summary>
        public virtual ISegyFile Read(Stream stream, int traceCount, IReadingProgress progress = null)
        {
            using (var reader = new BinaryReader(stream))
            {
                var fileHeader = ReadFileHeader(reader);
                var traces = new List<ITrace>();
                for (var i = 0; i < traceCount; i++)
                {
                    if (progress != null)
                    {
                        // TODO: Check if stream.Length breaks when streaming from web
                        var percentage = (int) (100*stream.Position/stream.Length);
                        progress.ReportProgress(percentage);
                        if (progress.CancellationPending)
                            break;
                    }
                    var trace = ReadTrace(reader, fileHeader.SampleFormat, fileHeader.IsLittleEndian);
                    if (trace == null)
                        break;
                    traces.Add(trace);
                }
                return new SegyFile {Header = fileHeader, Traces = traces};
            }
        }

        /// <summary>
        ///     Given a BinaryReader, reads the SEGY File Header into memory.
        ///     Asummes the BinaryReader is at the start of the file.
        /// </summary>
        public virtual IFileHeader ReadFileHeader(BinaryReader reader)
        {
            var text = ReadTextHeader(reader);
            var header = ReadBinaryHeader(reader) as FileHeader;
            // ReSharper disable once PossibleNullReferenceException
            header.Text = text;
            return header;
        }

        /// <summary>
        ///     Given a file path reads the text header from the beginning
        ///     of the SEGY file.
        /// </summary>
        public virtual string ReadTextHeader(string path)
        {
            using (var stream = File.OpenRead(path))
                return ReadTextHeader(stream);
        }

        /// <summary>
        ///     Given a stream reads the text header.
        ///     Assumes the stream is at the start of the file.
        /// </summary>
        public virtual string ReadTextHeader(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
                return ReadTextHeader(reader);
        }

        /// <summary>
        ///     Given a BinaryReader reads the text header.
        ///     Assumes the BinaryReader is at the start of the file.
        /// </summary>
        public virtual string ReadTextHeader(BinaryReader reader)
        {
            var textHeaderLength = Options.TextHeaderColumnCount*Options.TextHeaderRowCount;
            var bytes = reader.ReadBytes(textHeaderLength);
            var text = (bytes[0] == 'C') || Options.IsEbcdic == false
                ? Encoding.Default.GetString(bytes)
                : IbmConverter.ToString(bytes);
            return Options.TextHeaderInsertNewLines ? InsertNewLines(text) : text;
        }

        #endregion

        #region Already in progress: Methods that start reading from the current location in the stream

        /// <summary>
        ///     Given a BinaryReader, reads the binary header.
        ///     Assumes that the binary header is the next item to be read.
        /// </summary>
        public virtual IFileHeader ReadBinaryHeader(BinaryReader reader)
        {
            var binaryHeaderBytes = reader.ReadBytes(BinaryHeaderSize);
            var byte0 = binaryHeaderBytes[SampleFormatIndex];
            var byte1 = binaryHeaderBytes[SampleFormatIndex + 1];
            var isLittleEndian = (byte1 == 0);
            var sampleFormat = isLittleEndian
                ? (FormatCode) byte0
                : (FormatCode) byte1;

            // Read the binary header information
            var jobNumber = ToInt32(binaryHeaderBytes, 0, isLittleEndian);
            var lineNumber = ToInt32(binaryHeaderBytes, 4, isLittleEndian);
            var reelNumber = ToInt32(binaryHeaderBytes, 8, isLittleEndian);
            var dataTracesPerRecord = ToInt16(binaryHeaderBytes, 12, isLittleEndian);
            var auxTracesPerRecord = ToInt16(binaryHeaderBytes, 14, isLittleEndian);
            var reelSampleInterval = ToInt16(binaryHeaderBytes, 16, isLittleEndian);
            var fieldSampleInterval = ToInt16(binaryHeaderBytes, 18, isLittleEndian);
            var nDataPerTraceReel = ToInt16(binaryHeaderBytes, 20, isLittleEndian);
            var nDataPerTraceField = ToInt16(binaryHeaderBytes, 22, isLittleEndian);
            var sampleFormatCode = ToInt16(binaryHeaderBytes, 24, isLittleEndian);
            var cdpFold = ToInt16(binaryHeaderBytes, 26, isLittleEndian);
            var traceSortingCode = ToInt16(binaryHeaderBytes, 28, isLittleEndian);
            var verticalSumCode = ToInt16(binaryHeaderBytes, 30, isLittleEndian);
            var sweepFreqStart = ToInt16(binaryHeaderBytes, 32, isLittleEndian);
            var sweepFreqEnd = ToInt16(binaryHeaderBytes, 34, isLittleEndian);
            var sweepLength = ToInt16(binaryHeaderBytes, 36, isLittleEndian);
            var sweepTypeCode = ToInt16(binaryHeaderBytes, 38, isLittleEndian);
            var traceNumberOfSweepChannel = ToInt16(binaryHeaderBytes, 40, isLittleEndian);
            var sweepTraceTaperLengthStart = ToInt16(binaryHeaderBytes, 42, isLittleEndian);
            var sweepTraceTaperLengthEnd = ToInt16(binaryHeaderBytes, 44, isLittleEndian);
            var taperType = ToInt16(binaryHeaderBytes, 46, isLittleEndian);
            var correlatedDataTraces = ToInt16(binaryHeaderBytes, 48, isLittleEndian);
            var binaryGainRecovered = ToInt16(binaryHeaderBytes, 50, isLittleEndian);
            var amplitudeRecoveryMethod = ToInt16(binaryHeaderBytes, 52, isLittleEndian);
            var measurementSystem = ToInt16(binaryHeaderBytes, 54, isLittleEndian);
            var impulseSignal = ToInt16(binaryHeaderBytes, 56, isLittleEndian);
            var vibratoryPolarityCode = ToInt16(binaryHeaderBytes, 58, isLittleEndian);

            var binaryHeaderInfo = new BinaryHeader(jobNumber, lineNumber, reelNumber, dataTracesPerRecord,
                auxTracesPerRecord, reelSampleInterval, fieldSampleInterval, nDataPerTraceReel, nDataPerTraceField,
                sampleFormatCode, cdpFold, traceSortingCode, verticalSumCode, sweepFreqStart, sweepFreqEnd, sweepLength,
                sweepTypeCode, traceNumberOfSweepChannel, sweepTraceTaperLengthStart, sweepTraceTaperLengthEnd,
                taperType, correlatedDataTraces, binaryGainRecovered, amplitudeRecoveryMethod, measurementSystem,
                impulseSignal, vibratoryPolarityCode);

            return new FileHeader
            {
                SampleFormat = sampleFormat,
                IsLittleEndian = isLittleEndian,
                BinaryHeader = binaryHeaderInfo
            };
        }

        /// <summary>
        ///     Given a BinaryReader, reads the trace header.
        ///     Assumes that the trace header is the next item to be read.
        ///     Assumes that the byte order is Big Endian.
        /// </summary>
        public virtual ITraceHeader ReadTraceHeader(BinaryReader reader)
        {
            return ReadTraceHeader(reader, false);
        }

        /// <summary>
        ///     Given a BinaryReader, reads the trace header.
        ///     Assumes that the trace header is the next item to be read.
        /// </summary>
        public virtual ITraceHeader ReadTraceHeader(BinaryReader reader, bool isLittleEndian)
        {
            var traceHeader = new TraceHeader();
            var headerBytes = reader.ReadBytes(TraceHeaderSize);
            if (headerBytes.Length < TraceHeaderSize)
                return null;
            if (headerBytes.Length >= CrosslineNumberLocation + 3)
                traceHeader.CrosslineNumber = traceHeader.TraceNumber =
                    ToInt32(headerBytes, CrosslineNumberLocation - 1, isLittleEndian);
            if (headerBytes.Length >= InlineNumberLocation + 3)
                traceHeader.InlineNumber = ToInt32(headerBytes, InlineNumberLocation - 1, isLittleEndian);
            if (headerBytes.Length >= SampleCountIndex + 2)
                traceHeader.SampleCount = ToInt16(headerBytes, SampleCountIndex, isLittleEndian);

            traceHeader.Tracl = ToInt32(headerBytes, TraceHeaderBytePos.Tracl, isLittleEndian);
            traceHeader.Tracr = ToInt32(headerBytes, TraceHeaderBytePos.Tracr, isLittleEndian);
            traceHeader.Fldr = ToInt32(headerBytes, TraceHeaderBytePos.Fldr, isLittleEndian);
            traceHeader.Tracf = ToInt32(headerBytes, TraceHeaderBytePos.Tracf, isLittleEndian);
            traceHeader.Ep = ToInt32(headerBytes, TraceHeaderBytePos.Ep, isLittleEndian);
            traceHeader.Cdp = ToInt32(headerBytes, TraceHeaderBytePos.Cdp, isLittleEndian);
            traceHeader.Cdpt = ToInt32(headerBytes, TraceHeaderBytePos.Cdpt, isLittleEndian);
            traceHeader.Trid = ToInt16(headerBytes, TraceHeaderBytePos.Trid, isLittleEndian);
            traceHeader.Nvs = ToInt16(headerBytes, TraceHeaderBytePos.Nvs, isLittleEndian);
            traceHeader.Nhs = ToInt16(headerBytes, TraceHeaderBytePos.Nhs, isLittleEndian);
            traceHeader.Duse = ToInt16(headerBytes, TraceHeaderBytePos.Duse, isLittleEndian);
            traceHeader.Offset = ToInt32(headerBytes, TraceHeaderBytePos.Offset, isLittleEndian);
            traceHeader.Gelev = ToInt32(headerBytes, TraceHeaderBytePos.Gelev, isLittleEndian);
            traceHeader.Selev = ToInt32(headerBytes, TraceHeaderBytePos.Selev, isLittleEndian);
            traceHeader.Sdepth = ToInt32(headerBytes, TraceHeaderBytePos.Sdepth, isLittleEndian);
            traceHeader.Gdel = ToInt32(headerBytes, TraceHeaderBytePos.Gdel, isLittleEndian);
            traceHeader.Sdel = ToInt32(headerBytes, TraceHeaderBytePos.Sdel, isLittleEndian);
            traceHeader.Swdep = ToInt32(headerBytes, TraceHeaderBytePos.Swdep, isLittleEndian);
            traceHeader.Gwdep = ToInt32(headerBytes, TraceHeaderBytePos.Gwdep, isLittleEndian);
            traceHeader.Scalel = ToInt16(headerBytes, TraceHeaderBytePos.Scalel, isLittleEndian);
            traceHeader.Scalco = ToInt16(headerBytes, TraceHeaderBytePos.Scalco, isLittleEndian);
            traceHeader.Sx = ToInt32(headerBytes, TraceHeaderBytePos.Sx, isLittleEndian);
            traceHeader.Sy = ToInt32(headerBytes, TraceHeaderBytePos.Sy, isLittleEndian);
            traceHeader.Gx = ToInt32(headerBytes, TraceHeaderBytePos.Gx, isLittleEndian);
            traceHeader.Gy = ToInt32(headerBytes, TraceHeaderBytePos.Gy, isLittleEndian);
            traceHeader.Counit = ToInt16(headerBytes, TraceHeaderBytePos.Counit, isLittleEndian);

            return traceHeader;
        }

        /// <summary>
        ///     Reads the trace (header and sample values).
        ///     Assumes that the trace header is the next item to be read.
        /// </summary>
        public virtual ITrace ReadTrace(BinaryReader reader, FormatCode sampleFormat, bool isLittleEndian)
        {
            var header = ReadTraceHeader(reader, isLittleEndian);
            if (header == null)
                return null;
            var values = ReadTrace(reader, sampleFormat, header.SampleCount, isLittleEndian);
            return new Trace {Header = header, Values = values};
        }

        /// <summary>
        ///     Assuming the trace header has been read, reads the array of sample values
        /// </summary>
        public virtual IList<float> ReadTrace(BinaryReader reader, FormatCode sampleFormat, int sampleCount,
            bool isLittleEndian)
        {
            var trace = new float[sampleCount];
            try
            {
                for (var i = 0; i < sampleCount; i++)
                {
                    switch (sampleFormat)
                    {
                        case FormatCode.IbmFloatingPoint4:
                            trace[i] = reader.ReadSingleIbm();
                            break;
                        case FormatCode.IeeeFloatingPoint4:
                            trace[i] = isLittleEndian
                                ? reader.ReadSingle()
                                : ReadReversedSingle(reader);
                            break;
                        case FormatCode.TwosComplementInteger1:
                            trace[i] = ReadSignedByte(reader);
                            break;
                        case FormatCode.TwosComplementInteger2:
                            trace[i] = isLittleEndian
                                ? reader.ReadInt16()
                                : reader.ReadInt16BigEndian();
                            break;
                        case FormatCode.TwosComplementInteger4:
                            trace[i] = isLittleEndian
                                ? reader.ReadInt32()
                                : reader.ReadInt32BigEndian();
                            break;
                        default:
                            throw new NotSupportedException(
                                String.Format(
                                    "Unsupported sample format: {0}. Send an email to dev@segy.net to request support for this format.",
                                    sampleFormat));
                    }
                }
            }
            catch (EndOfStreamException)
            {
                /* Encountered end of stream before end of trace. Leave remaining trace samples as zero */
            }
            return trace;
        }

        #endregion

        #region Behind the Scenes

        private const int BinaryHeaderSize = 400;
        private const int TraceHeaderSize = 240;
        private const int SampleFormatIndex = 24;
        private const int SampleCountIndex = 114;

        private string InsertNewLines(string text)
        {
            var rows = Options.TextHeaderRowCount;
            var cols = Options.TextHeaderColumnCount;
            var result = new StringBuilder(text.Length + rows);
            for (var i = 0; i < 1 + text.Length/cols; i++)
            {
                var line = new string(text.Skip(cols*i).Take(cols).ToArray());
                result.AppendLine(line);
            }
            return result.ToString();
        }

        private static int ToInt16(byte[] bytes, int index, bool isLittleEndian)
        {
            return isLittleEndian
                ? BitConverter.ToInt16(bytes, index)
                : IbmConverter.ToInt16(bytes, index);
        }

        private static int ToInt32(byte[] bytes, int index, bool isLittleEndian)
        {
            return isLittleEndian
                ? BitConverter.ToInt32(bytes, index)
                : IbmConverter.ToInt32(bytes, index);
        }

        private static float ReadSignedByte(BinaryReader reader)
        {
            var b = reader.ReadByte();
            return b < 128 ? b : b - 256;
        }

        private static float ReadReversedSingle(BinaryReader reader)
        {
            var b = reader.ReadBytes(4).Reverse().ToArray();
            return BitConverter.ToSingle(b, 0);
        }

        #endregion
    }
}