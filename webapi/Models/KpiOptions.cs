namespace cndcAPI.Models
{
    public class KpiOptions
    {
        public Dictionary<string, KpiItem> Valores { get; set; } = new();
    }

    public class KpiItem
    {
        public string Valor { get; set; } = "";
        public string Texto { get; set; } = "";
    }
}
