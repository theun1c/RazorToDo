using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace RazorToDo.Models
{
    [Table("TodoItem")]
    public class TodoItem : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("title")]
        public string? Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("author")]
        public string? Author { get; set; }

        [Column("status")]
        public string? Status { get; set; } // пока что стринг, пушто возникали ошибки с десериализацией энама

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public override string ToString()
        {
            return $"{Id} {Title} {Description} {Author} {Status} {CreatedAt}";
        }
    }

    
}
