using AdvicerApp.Core.Entities;

namespace AdvicerApp.BL.ExternalServices.Interfaces;

public interface IJwtHandler
{
    Task<string> CreateJwtToken(User user,int hours);
}
