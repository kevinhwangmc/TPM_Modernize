using HPNSDataGenerator.util;

namespace HPNSDataGenerator
{
    [Serializable]
    public class AnalysisTblV3
    {
        public byte[] GetBytes()
        {
            byte[][] bytes =
            {
                ByteHelper.GetByteInt(BIN),
                ByteHelper.GetByteInt(RecvTime),
                ByteHelper.GetByteInt(RespTime),
                ByteHelper.GetByteString(VersionCode),
                ByteHelper.GetByteString(TransCode),
                ByteHelper.GetRejectCode(RejectCode),
                ByteHelper.GetByteString(PCN)
            };
            return ByteHelper.Combine(bytes);
        }
        public int BIN { get; set; } = 5947;
        public int RecvTime { get; set; } = 1517633343;
        public short RespTime { get; set; } = 12;
        public string VersionCode { get; set; } = "D0";
        public string TransCode { get; set; } = "B2";
        public string RejectCode { get; set; } = "87"; // 3 bytes
        public string PCN { get; set; } = "CLA";
    }

}