namespace Datasource.Ef.Models;
public class User {
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
