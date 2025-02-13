using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Entities.Common;
using AdvicerApp.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AdvicerApp.BL.Services.Implements;

public class OwnerApproveService(IOwnerRequestRepository _repo, UserManager<User> _userManager,IWebHostEnvironment _env,ICurrentUser _user) : IOwnerApproveService
{
    private string _userId = _user.GetId();
    public async Task RequestApprovalAsync( IFormFile document)
    {
        var ownerId = _userId;
        var owner  = await _userManager.FindByIdAsync(ownerId);
        if (owner == null) throw new NotFoundException<User>("Owner is not found");

        if (await _repo.IsExistAsync(x => x.OwnerId == ownerId)) throw new ExistsException<User>("Owner request already submitted");
        var documentUrl = await document.UploadAsync(_env.WebRootPath, "imgs", "ownerDocument");

        var request = new OwnerRequest
        {
            OwnerId = ownerId,
            DocumentUrl = documentUrl,
            IsApproved = false
        };
        await _repo.AddAsync(request);
    }
    public async Task ApproveOwnerAsync(string ownerId)
    {
        var request = await _repo.GetFirstAsync(x => x.OwnerId == ownerId && !x.IsApproved, x => x, false, false);
        if (request == null)
            throw new NotFoundException<OwnerRequest>("Owner request not found.");

        var owner = await _userManager.FindByIdAsync(ownerId);
        if (owner == null) throw new NotFoundException<User>("Owner is not found");

        var result = await _userManager.AddToRoleAsync(owner, "Owner");
        if (!result.Succeeded)
            throw new FailedRequestException("Failed to assign Owner role");

        request.IsApproved = true;
    }

}
