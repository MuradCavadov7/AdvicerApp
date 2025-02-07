using AdvicerApp.Core.Enums;

namespace AdvicerApp.BL.Helper;

public class RoleConstants
{
    public const string AccessToCategory = nameof(Role.Admin);
    public const string AccessToRating = nameof(Role.User);
    public const string AccessToRestaurant = nameof(Role.Owner);

}
