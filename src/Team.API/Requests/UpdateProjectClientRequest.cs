namespace Team.API.Requests
{
    public class UpdateProjectClientRequest
    {
        public string ProjectClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
