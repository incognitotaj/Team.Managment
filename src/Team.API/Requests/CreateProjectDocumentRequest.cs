namespace Team.API.Requests
{
    public class CreateProjectDocumentRequest
    {
        public string Title { get; set; }
        public IFormFile Document { get; set; }
        public string Detail { get; set; }
    }
}
