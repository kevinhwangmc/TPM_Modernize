namespace HPNSDataGenerator
{
    [Serializable]
    public class DataPacketWrapper
    {
        //STX
        public string MessageLength { get; set; } = "15067";
        public string VariableHeaderLength { get; set; } = "016";
        public string variableHeader { get; set; } = "PSO706~~10TP0000";
        public AnalysisData Data { get; set; }
        //ETX

    }
}