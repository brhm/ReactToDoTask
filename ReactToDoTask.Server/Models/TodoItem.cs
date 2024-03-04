namespace ReactToDoTask.Server.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public bool Completed { get; set; }
    }
    
}
