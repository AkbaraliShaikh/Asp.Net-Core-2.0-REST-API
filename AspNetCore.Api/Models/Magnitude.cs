namespace AspNetCore.Api.Models
{
    public class Magnitude
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public string Acronym { get; set; }
        public string Currency { get; set; }
        public bool IsActive { get; set; }
    }
}
