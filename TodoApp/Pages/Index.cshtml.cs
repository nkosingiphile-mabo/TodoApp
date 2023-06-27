using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Services;
using TodoApp.Services.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace TodoApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TodoAppService _todoAppService;
        public List<TodoItemDto> TodoItems { get; set; }

        public IndexModel(TodoAppService todoAppService)
        {
        }

        public async Task OnGetAsync()
        {
        }
    }
}
