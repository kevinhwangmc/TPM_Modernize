namespace HPNSDataGenerator
{
    [Serializable]
    public class AnalysisData
    {
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