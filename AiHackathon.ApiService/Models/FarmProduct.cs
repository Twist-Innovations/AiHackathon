namespace AiHackathon.ApiService.Models
{
    public class FarmProduct
    {
        public string Id { get; set; } = string.Empty;
        public double Quantity { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FarmId { get; set; } = string.Empty;
    }
}
