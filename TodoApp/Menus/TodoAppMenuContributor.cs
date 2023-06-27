using TodoApp.Localization;
using Volo.Abp;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace TodoApp.Menus;

public class TodoAppMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<TodoAppResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                TodoAppMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        var isAuthenticated = context.GetHttpContext().User.Identity.IsAuthenticated;

        if (isAuthenticated)
        {
            context.Menu.Items.Add(
                new ApplicationMenuItem(
                    TodoAppMenus.TodoList,
                    l["Todo List"],
                    "/TodoList", 
                    icon: "fas fa-tasks",
                    order: 1
                )
            );

            context.Menu.Items.Add(
               new ApplicationMenuItem(
                   TodoAppMenus.MyAccount,
                   l["My Account"],
                   "/Account/Manage",
                   icon: "fas fa-cog",
                   order: 2
               )
           );

            context.Menu.Items.Add(
               new ApplicationMenuItem(
                   TodoAppMenus.Logout,
                   l["Logout"],
                   "/Logout",
                   icon: "fas fa-power-off",
                   order: 3
               )
           );
        }

        if (TodoAppModule.IsMultiTenant)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        return Task.CompletedTask;
    }
}
