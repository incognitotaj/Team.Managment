namespace Team.API.Requests
{
    public class UpdateProjectDocumentRequest
    {
        public string ProjectDocumentId { get; set; }
        public string Title { get; set; }
        public IFormFile Document { get; set; }
        public string Detail { get; set; }
    }
}
