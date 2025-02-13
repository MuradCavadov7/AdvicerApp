using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AdvicerApp.BL.Services.Interface;

public interface IOwnerApproveService
{
    Task RequestApprovalAsync(IFormFile document);
    Task ApproveOwnerAsync(string ownerId);
}
