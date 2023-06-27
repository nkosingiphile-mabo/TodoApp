using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Services;
using TodoApp.Services.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace TodoApp.Pages
{
    public class TodoListModel : PageModel, ITransientDependency
    {
        private readonly TodoAppService _todoAppService;
        private readonly ICurrentUser _currentUser;
        public List<TodoItemDto> TodoItems { get; set; }
        public string UserId { get; set; }

        public TodoListModel(TodoAppService todoAppService, ICurrentUser currentUser)
        {
            _todoAppService = todoAppService;
            _currentUser = currentUser;
        }

        public async Task OnGetAsync()
        {
            // Get the logged-in user ID
            UserId = _currentUser.Id.ToString();
            // Use the user ID to retrieve the list of todo items
            TodoItems = await _todoAppService.GetListAsync(Guid.Parse(UserId));
        }
    }
}
