namespace Team.API.Requests
{
    public class CreateProjectResourceRequest
    {
        public string ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
