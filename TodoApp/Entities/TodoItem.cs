using Volo.Abp.Domain.Entities;

namespace TodoAppEntities;

public class TodoItem : BasicAggregateRoot<Guid>
{
    public string Text { get; set; }
    public Guid UserId { get; set; } // User ID attribute of a logged in user
}
