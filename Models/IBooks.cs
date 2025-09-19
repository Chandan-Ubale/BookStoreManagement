namespace Models
{
    public interface IBook
    {
        string? Id { get; set; }
        string? Title { get; set; }
        string? Author { get; set; }
        decimal Price { get; set; }
    }
}
