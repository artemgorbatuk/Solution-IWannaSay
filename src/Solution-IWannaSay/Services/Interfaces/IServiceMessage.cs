namespace Services.Interfaces;
public interface IServiceMessage {
    Task SendMessageAsync(string message);
    IEnumerable<string> GetMessages();
}