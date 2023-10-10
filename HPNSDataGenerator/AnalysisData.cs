using HPNSDataGenerator.util;

namespace HPNSDataGenerator
{
    [Serializable]
    public class AnalysisData
    {
        public AnalysisData()
        {
            var lstData = new List<AnalysisTblV3>();
            for(int i = 0; i < 750; i++)
                lstData.Add(new AnalysisTblV3());

            AnalysisTbl = lstData.ToArray();
        }

        public byte[] GetBytes()
        {
            var lstData = new List<byte[]>
            {
                ByteHelper.GetByteString(RecType),
                ByteHelper.GetByteString(CommandStr),
                ByteHelper.GetByteInt(Sid),
                ByteHelper.GetByteString(HeaderVersion),
                ByteHelper.GetByteInt(SystemNum),
                ByteHelper.GetByteInt(StartEntry),
                ByteHelper.GetByteInt(EndEntry)
            };

            //Current Time 16 Bytes
            short year = Convert.ToInt16(DateTime.Now.Year);
            short month = Convert.ToInt16(DateTime.Now.Month);
            short day = Convert.ToInt16(DateTime.Now.Day);
            short hour = Convert.ToInt16(DateTime.Now.Hour);
            short minute = Convert.ToInt16(DateTime.Now.Minute);
            short second = Convert.ToInt16(DateTime.Now.Second);
            short milisecond = Convert.ToInt16(DateTime.Now.Millisecond);

            lstData.Add(ByteHelper.GetByteInt(year));
            lstData.Add(ByteHelper.GetByteInt(month));
            lstData.Add(ByteHelper.GetByteInt(day));
            lstData.Add(ByteHelper.GetByteInt(hour));
            lstData.Add(ByteHelper.GetByteInt(minute));
            lstData.Add(ByteHelper.GetByteInt(second));
            lstData.Add(ByteHelper.GetByteInt(milisecond));
            lstData.Add(new byte[2]);

            //year, month, day, hour, minute, seconds, miliseconds

            //Timestamp 6 Bytes
            lstData.Add(ByteHelper.GetByteTimeStamp());

            lstData.Add(ByteHelper.GetByteInt(CurrentConnections));
            lstData.Add(ByteHelper.GetByteInt(MaxConnections));
            lstData.Add(ByteHelper.GetByteInt(HostPort));

            //foreach (var data in AnalysisTbl)
            //    lstData.Add(data.GetBytes());

            //For loop - last transaction should be loaded 19 bytes for HPNS bug.
            for (int i = 0; i < AnalysisTbl.Length; i++)
            {
                if(i != AnalysisTbl.Length - 1)
                    lstData.Add(AnalysisTbl[i].GetBytes());
                else
                    lstData.Add(ByteHelper.GetByteLastTransaction(AnalysisTbl[i].GetBytes()));
            }
            return ByteHelper.Combine(lstData.ToArray());
        }

        public string RecType { get; set; } = "ZX";
        public string CommandStr { get; set; } = "18APOL";
        public short Sid { get; set; } = 6975;
        public string HeaderVersion { get; set; } = "V3";
        public short SystemNum { get; set; } = 0;
        public short StartEntry { get; set; } = 108;
        public short EndEntry { get; set; } = 63;
        public short[] CurrentTime { get; set; } = new short[8];
        public short[] TimeStamp { get; set;} = new short[3];
        public short CurrentConnections { get; set; } = 22;
        public short MaxConnections { get; set; } = 41;
        public int HostPort { get; set; } = 6975;

        public AnalysisTblV3[] AnalysisTbl { get; set; } 
    }
}