
//using TodoApp.Entities;
using System.Linq;
using TodoApp.Services.Dtos;
using TodoAppEntities;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;


namespace TodoApp.Services;

public class TodoAppService : ApplicationService
{
    private readonly IRepository<TodoItem, Guid> _todoItemRepository;
    private readonly ICurrentUser _currentUser;

    public TodoAppService(IRepository<TodoItem, Guid> todoItemRepository, ICurrentUser currentUser)
    {
        _todoItemRepository = todoItemRepository;
        _currentUser = currentUser;
    }

    // TODO: Implement the methods here...
    public async Task<List<TodoItemDto>> GetListAsync(Guid userId)
    {
        var items = await _todoItemRepository
            .GetListAsync(item => item.UserId == userId);

        return items
            .Select(item => new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text
            })
            .ToList();
    }

    public async Task<TodoItemDto> CreateAsync(string text, Guid userId)
    {
        var todoItem = new TodoItem
        {
            Text = text,
            UserId = userId
        };

        todoItem = await _todoItemRepository.InsertAsync(todoItem);

        return new TodoItemDto
        {
            Id = todoItem.Id,
            Text = todoItem.Text
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        await _todoItemRepository.DeleteAsync(id);
    }

    public async Task<TodoItemDto> UpdateAsync(Guid id, string newText)
    {
        var todoItem = await _todoItemRepository.GetAsync(id);
        if (todoItem == null)
        {
            throw new EntityNotFoundException(typeof(TodoItem), id);
        }

        todoItem.Text = newText;
        await _todoItemRepository.UpdateAsync(todoItem);

        return new TodoItemDto
        {
            Id = todoItem.Id,
            Text = todoItem.Text
        };
    }

}
