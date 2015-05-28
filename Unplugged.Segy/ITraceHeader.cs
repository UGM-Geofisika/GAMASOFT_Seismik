namespace Unplugged.Segy
{
    public interface ITraceHeader
    {
        int SampleCount { get; }
        int TraceNumber { get; }
        int InlineNumber { get; }
        int CrosslineNumber { get; }

        /// <summary>
        ///     Trace sequence number within line
        /// </summary>
        int Tracl { get; }

        /// <summary>
        ///     Trace sequence number within reel
        /// </summary>
        int Tracr { get; }

        /// <summary>
        ///     Original field record number
        /// </summary>
        int Fldr { get; }

        /// <summary>
        ///     Trace sequence number within original field record
        /// </summary>
        int Tracf { get; }

        /// <summary>
        ///     Energi source point number
        /// </summary>
        int Ep { get; }

        /// <summary>
        ///     CDP ensemble number
        /// </summary>
        int Cdp { get; }

        /// <summary>
        ///     Trace sequence number within CDP ensemble
        /// </summary>
        int Cdpt { get; }

        //      0 = Unknown
        //      1 = seismic data
        //      2 = dead
        //      3 = dummy
        //      4 = time break
        //      5 = uphole
        //      6 = sweep
        //      7 = timing
        //      8 = water break
        //      9 = Near-field gun signature
        //      10 = Far-field gun signature
        //      11 = Seismic pressure sensor
        //      12 = Multicomponent seismic sensor - Vertical component
        //      13 = Multicomponent seismic sensor - Cross-line component
        //      14 = Multicomponent seismic sensor - In-line component
        //      15 = Rotated multicomponent seismic sensor - Vertical component
        //      16 = Rotated multicomponent seismic sensor - Transverse component
        //      17 = Rotated multicomponent seismic sensor - Radial component
        //      18 = Vibrator reaction mass
        //      19 = Vibrator baseplate
        //      20 = Vibrator estimated ground force
        //      21 = Vibrator reference
        //      22 = Time-velocity pairs
        //      23+ = optional use
        /// <summary>
        ///     Trace identification code
        ///     -1 = Other
        /// </summary>
        int Trid { get; }

        /// <summary>
        ///     Number of vertically summed traces yielding this trace
        /// </summary>
        int Nvs { get; }

        /// <summary>
        ///     Number of horizontally stacked traces yielding this trace
        /// </summary>
        int Nhs { get; }

        /// <summary>
        ///     Data Use
        ///     1 = production
        ///     2 = test
        /// </summary>
        int Duse { get; }

        /// <summary>
        ///     Distance from source point to receiver group
        /// </summary>
        int Offset { get; }

        /// <summary>
        ///     Receiver group elevation
        /// </summary>
        int Gelev { get; }

        /// <summary>
        ///     Surface elevation at source
        /// </summary>
        int Selev { get; }

        /// <summary>
        ///     Source depth below surface
        /// </summary>
        int Sdepth { get; }

        /// <summary>
        ///     Datum elevation at receiver group
        /// </summary>
        int Gdel { get; }

        /// <summary>
        ///     Datum elevation at source
        /// </summary>
        int Sdel { get; }

        /// <summary>
        ///     Water depth at source
        /// </summary>
        int Swdep { get; }

        /// <summary>
        ///     Water depth at receiver group
        /// </summary>
        int Gwdep { get; }

        /// <summary>
        ///     Scalar for elevations and depths (+ = multiplier, - = divisor)
        /// </summary>
        int Scalel { get; }

        /// <summary>
        ///     Scalar for coordinates (+ = multiplier, - = divisor)
        /// </summary>
        int Scalco { get; }

        /// <summary>
        ///     X source coordinate
        /// </summary>
        int Sx { get; }

        /// <summary>
        ///     Y source coordinate
        /// </summary>
        int Sy { get; }

        /// <summary>
        ///     X receiver group coordinate
        /// </summary>
        int Gx { get; }

        /// <summary>
        ///     Y receiver group coordinate
        /// </summary>
        int Gy { get; }

        /// <summary>
        ///     Coordinate units
        ///     1 = Length in meters or feet
        ///     2 = arc seconds
        ///     3 = decimal degrees
        ///     4 = degrees, minutes, seconds
        /// </summary>
        int Counit { get; }

        /// <summary>
        ///     Weathering velocity
        /// </summary>
        int Wevel { get; }

        /// <summary>
        ///     Subweathering velocity
        /// </summary>
        int Swevel { get; }

        /// <summary>
        ///     Uphole time at source
        /// </summary>
        int Sut { get; }

        /// <summary>
        ///     Uphole time at receiver group
        /// </summary>
        int Gut { get; }

        /// <summary>
        ///     Source static correction
        /// </summary>
        int Sstat { get; }

        /// <summary>
        ///     Receiver group static correction
        /// </summary>
        int Gstat { get; }

        /// <summary>
        ///     Total static applied
        /// </summary>
        int Tstat { get; }

        /// <summary>
        ///     Lag time between end of header and time break in milliseconds
        /// </summary>
        int Laga { get; }

        /// <summary>
        ///     Lag time between time break and shot in milliseconds
        /// </summary>
        int Lagb { get; }

        /// <summary>
        ///     Lag time between shot and recording start in milliseconds
        /// </summary>
        int Delrt { get; }

        /// <summary>
        ///     Start of mute time
        /// </summary>
        int Muts { get; }

        /// <summary>
        ///     End of mute time
        /// </summary>
        int Mute { get; }

        /// <summary>
        ///     Number of samples in this trace
        /// </summary>
        int Ns { get; }

        /// <summary>
        ///     Sample interval of this trace in microseconds
        /// </summary>
        int Dt { get; }

        //1 = fixed
        //2 = binary
        //3 = floating point
        //4+ = optional use
        /// <summary>
        ///     Field instrument gain type code:
        /// </summary>
        int Gain { get; }

        /// <summary>
        ///     Instrument gain constant
        /// </summary>
        int Igc { get; }

        /// <summary>
        ///     Intrument early gain in decibels
        /// </summary>
        int Igi { get; }

        /// <summary>
        ///     Correlated (1 = no, 2 = yes)
        /// </summary>
        int Corr { get; }

        /// <summary>
        ///     Sweep frequency at start
        /// </summary>
        int Sfs { get; }

        /// <summary>
        ///     Sweep fequency at end
        /// </summary>
        int Sfe { get; }

        /// <summary>
        ///     Sweep length in milliseconds
        /// </summary>
        int Slen { get; }

        //1 = linear
        //2 = parabolic
        //3 = exponential
        //4 = other
        /// <summary>
        ///     Sweep type code:
        /// </summary>
        int Styp { get; }

        /// <summary>
        ///     Sweep taper trace length at start in milliseconds
        /// </summary>
        int Stas { get; }

        /// <summary>
        ///     Sweep taper trace length at end in milliseconds
        /// </summary>
        int Stae { get; }

        //1 = linear
        //2 = cosine squared
        //3 = other
        /// <summary>
        ///     Taper type code:
        /// </summary>
        int Tatyp { get; }

        /// <summary>
        ///     Alias filter frequency
        /// </summary>
        int Afilf { get; }

        /// <summary>
        ///     Alias filter slope
        /// </summary>
        int Afils { get; }

        /// <summary>
        ///     Notch filter frequency
        /// </summary>
        int Nofilf { get; }

        /// <summary>
        ///     Notch filter slope
        /// </summary>
        int Nofils { get; }

        /// <summary>
        ///     Low cut frequency
        /// </summary>
        int Lcf { get; }

        /// <summary>
        ///     High cut frequency
        /// </summary>
        int Hcf { get; }

        /// <summary>
        ///     Low cut slope
        /// </summary>
        int Lcs { get; }

        /// <summary>
        ///     High cut slope
        /// </summary>
        int Hcs { get; }

        /// <summary>
        ///     Year data recorded
        /// </summary>
        int Year { get; }

        /// <summary>
        ///     Day of year
        /// </summary>
        int Day { get; }

        /// <summary>
        ///     Hour of day (24-hour clock)
        /// </summary>
        int Hour { get; }

        /// <summary>
        ///     Minute of hour
        /// </summary>
        int Min { get; }

        /// <summary>
        ///     Second of minute
        /// </summary>
        int Sec { get; }

        /// <summary>
        ///     Time basis (1 = local, 2 = GMT, 3 = other)
        /// </summary>
        int Timbas { get; }

        /// <summary>
        ///     Trace weighting factor for fixed-point format data
        /// </summary>
        int Trwf { get; }

        /// <summary>
        ///     Geophone group number of roll switch position one
        /// </summary>
        int Grnors { get; }

        /// <summary>
        ///     Geophone group number of first trace of original field record
        /// </summary>
        int Grnofr { get; }

        /// <summary>
        ///     Geophone group number of last trace of original field record
        /// </summary>
        int Grnlof { get; }

        /// <summary>
        ///     Gap size (total number of groups dropped)
        /// </summary>
        int Gaps { get; }

        /// <summary>
        ///     Overtravel associated with taper (1 = down/behind, 2 = up/ahead)
        /// </summary>
        int Otrav { get; }
    }
}