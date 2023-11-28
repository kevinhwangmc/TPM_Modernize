using HPNSDataGenerator.util;

namespace HPNSDataGenerator
{
    [Serializable]
    public class AnalysisTblV3
    {
        public AnalysisTblV3(bool isF6)
        {
            var lstBin = ByteHelper.GetCVSBIN();
            var lstRejectCode = ByteHelper.GetRejectCode();
            var lstTransCode = ByteHelper.GetTransCode();
            if (isF6)
                VersionCode = "F6";
            else
                VersionCode = "D0";

            Random rnd = new Random();

            if (isF6) // 8 digit numbers for F6
                BIN = Convert.ToInt32(lstBin[rnd.Next(856)]) * 100;
            else
                BIN = Convert.ToInt32(lstBin[rnd.Next(856)]);

            var randomBool = rnd.Next(0, 2) > 0;

            if (randomBool)
                RejectCode = lstRejectCode[rnd.Next(1421)];
            else
                RejectCode = "";

            TransCode = lstTransCode[rnd.Next(17)];
            RespTime = Convert.ToInt16(rnd.Next(500));
            DateTime currentTime = DateTime.UtcNow;
            RecvTime = Convert.ToInt32(((DateTimeOffset)currentTime).ToUnixTimeSeconds());
        }
        public byte[] GetBytes()
        {
            byte[][] bytes =
            {
                ByteHelper.GetByteInt(BIN),
                ByteHelper.GetByteInt(RecvTime),
                ByteHelper.GetByteInt(RespTime),
                ByteHelper.GetByteString(VersionCode),
                ByteHelper.GetByteString(TransCode),
                ByteHelper.GetByteRejectCode(RejectCode),
                ByteHelper.GetByteString(PCN)
            };
            return ByteHelper.Combine(bytes);
        }
        public int BIN { get; set; }
        public int RecvTime { get; set; }
        public short RespTime { get; set; }
        public string VersionCode { get; set; }
        public string TransCode { get; set; }
        public string RejectCode { get; set; }
        public string PCN { get; set; } = "CLA";
    }

}