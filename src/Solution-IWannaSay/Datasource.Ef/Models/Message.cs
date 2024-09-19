namespace Datasource.Ef.Models;

public class Message {
    public int Id { get; set; }
    public string Text { get; set; } = default!;

    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;
}