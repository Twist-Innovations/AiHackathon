namespace AiHackathon.ApiService.ApiRequests
{
    public class CreateFramProductRequest
    {
        public double Quantity { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FarmId { get; set; } = string.Empty;
    }
}
