using System.Collections.Generic;
using System.Drawing;
using Unplugged.Segy;

namespace Gamaseis
{
    static class SeismicFileHandler
    {
        public static ISegyFile Read(string filename)
        {
            var reader = new SegyReader();
            var line = reader.Read(filename);
            return line;
        }

        public static IEnumerable<ITrace> GetTraces(ISegyFile file)
        {
            return file.Traces;
        }

        public static Bitmap GetAllTracesBitmap(ISegyFile file)
        {
            var imgwriter = new ImageWriter();
            return imgwriter.GetBitmap(file);
        }
    }
}
