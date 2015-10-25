namespace Gamaseis
{
    public class SegyFileHeaderItem
    {
        public string Description { get; set; }
        public int Value { get; set; }
        public string BytePosition { get; set; }

        public SegyFileHeaderItem(string desc, int value, string bytePos)
        {
            Description = desc;
            Value = value;
            BytePosition = bytePos;
        }
    }
}
