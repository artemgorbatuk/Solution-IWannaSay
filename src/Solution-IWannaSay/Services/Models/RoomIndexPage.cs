namespace Services.Models;
public class RoomIndexPage {
    public Guid Id { get; set; } = Guid.NewGuid();
    public RoomIndexUserModel User { get; set; } = default!;
    public IEnumerable<RoomIndexGroupModel> Groups { get; set; } = [];
    public IEnumerable<RoomIndexMessageModel> Messages { get; set; } = [];
}

public class RoomIndexUserModel {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
}

public class RoomIndexGroupModel {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
}

public class RoomIndexMessageModel {
    public Guid Id { get; set; }
    public string Text { get; set; } = default!;
}