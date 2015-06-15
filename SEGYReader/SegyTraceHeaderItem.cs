namespace SegyView
{
    internal class SegyTraceHeaderItem
    {
        public SegyTraceHeaderItem(string desc, int value, string bytePos)
        {
            Description = desc;
            Value = value;
            BytePosition = bytePos;
        }

        public string Description { get; set; }
        public int Value { get; set; }
        public string BytePosition { get; set; }
    }
}