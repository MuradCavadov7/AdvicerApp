using AdvicerApp.Core.Entities;

namespace AdvicerApp.BL.ExternalServices.Interfaces;

public interface IJwtHandler
{
    string CreateJwtToken(User user,int hours);
}
