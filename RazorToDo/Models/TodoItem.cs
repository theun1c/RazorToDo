namespace RazorToDo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;   

    }
}
