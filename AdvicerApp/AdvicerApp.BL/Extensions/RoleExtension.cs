using AdvicerApp.Core.Enums;

namespace AdvicerApp.BL.Extensions;

public static class RoleExtension
{
    public static string GetRole(this Role role)
    {
        return role switch
        {
            Role.Admin => nameof(Role.Admin),
            Role.Owner => nameof(Role.Owner),
            Role.User => nameof(Role.User)
        };
    }
}
