using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unplugged.Segy
{
    class TraceHeaderBytePos
    {
        internal static int Tracl = 0;
        internal static int Tracr = 4;
        internal static int Fldr = 8;
        internal static int Tracf = 12;
        internal static int Offset = 36;
        internal static int Sx = 72;
        internal static int Sy = 76;
        internal static int Gx = 80;
        internal static int Gy = 84;
        internal static int Counit = 88;
    }
}
