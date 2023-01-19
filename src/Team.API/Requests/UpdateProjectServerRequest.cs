namespace Team.API.Requests
{
    public class UpdateProjectServerRequest
    {
        public string ProjectServerId { get; set; }        
        public string Title { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
