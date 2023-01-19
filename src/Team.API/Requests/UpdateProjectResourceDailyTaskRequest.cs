namespace Team.API.Requests
{
    public class UpdateProjectResourceDailyTaskRequest
    {
        public string ProjectResourceDailyTaskId { get; set; }
        public string ProjectResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatus { get; set; }
    }
}
