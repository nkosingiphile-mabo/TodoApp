namespace TodoApp.Services.Dtos;

public class TodoItemDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid UserId { get; set; } // User ID attribute of a logged in user
}
