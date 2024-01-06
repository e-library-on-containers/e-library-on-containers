namespace Books.Infrastructure.Models;

public class Audiobook
{
    public int Id { get; set; }
    public bool InPreview { get; set; }
    public int BookId { get; set; }
    public int Duration { get; set; }
}