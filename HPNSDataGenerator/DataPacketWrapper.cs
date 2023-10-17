using HPNSDataGenerator.util;
using System.Security.AccessControl;
using System.Security.Cryptography;

namespace HPNSDataGenerator
{
    [Serializable]
    public class DataPacketWrapper : IDisposable
    {
        public DataPacketWrapper(bool isF6)
        {
            Data = new AnalysisData(isF6);
        }

        ~DataPacketWrapper()
        {
            Array.Clear(Data.AnalysisTbl);
            Dispose(false);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                ReleaseManagedResources();
            }
            // free native resources if there are any.
            ReleaseUnmangedResources();
        }
        void ReleaseManagedResources()
        {
            //Console.WriteLine("Releasing Managed Resources");
            //if (Data != null)
            //{
            //    Data.Dispose();
            //}
        }

        void ReleaseUnmangedResources()
        {
            Array.Clear(Data.AnalysisTbl);
            //Console.WriteLine("Releasing Unmanaged Resources");
        }

        public byte[] STX { get; set; } = { 0x02 };
        public string MessageLength { get; set; } = "15067";
        public string VariableHeaderLength { get; set; } = "016";
        public string variableHeader { get; set; } = "PSO706~~10TP0000";
        public AnalysisData Data { get; set; }
        public byte[] ETX { get; set; } = { 0x03 };

    }
}