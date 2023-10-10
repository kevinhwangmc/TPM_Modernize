using HPNSDataGenerator.util;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace HPNSDataGenerator
{
    [Serializable]
    public class DataPacketWrapper
    {
        public DataPacketWrapper()
        {
            Data = new AnalysisData();
        }
        public byte[] GetBytes()
        {
            var lstData = new List<byte[]>
            {
                STX,
                ByteHelper.GetByteString(MessageLength),
                ByteHelper.GetByteString(VariableHeaderLength),
                ByteHelper.GetByteString(variableHeader),
                Data.GetBytes(),
                ETX
            };

            return ByteHelper.Combine(lstData.ToArray());
        }

        public byte[] STX { get; set; } = { 0x02 };
        public string MessageLength { get; set; } = "15067";
        public string VariableHeaderLength { get; set; } = "016";
        public string variableHeader { get; set; } = "PSO706~~10TP0000";
        public AnalysisData Data { get; set; }
        public byte[] ETX { get; set; } = { 0x03 };

    }
}