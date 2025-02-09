using AdvicerApp.BL.DTOs.MenuItemDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace AdvicerApp.BL.Services.Implements;

public class MenuItemService(IMenuItemRepository _repo, IMapper _mapper, IMenuRepository _menuRepo, IWebHostEnvironment _env) : IMenuItemService
{
    public async Task<int> CreateAsync(MenuItemCreateDto dto)
    {
        if (dto.File != null)
        {
            if (!dto.File.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.File.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }
        var menuItem = _mapper.Map<MenuItem>(dto);
        if (!await _menuRepo.IsExistAsync(dto.MenuId)) throw new NotFoundException<Menu>("Menu is not found");
        menuItem.Image = await dto.File!.UploadAsync(_env.WebRootPath, "imgs", "menuItem");
        await _repo.AddAsync(menuItem);
        await _repo.SaveAsync();
        return menuItem.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var menuItem = await _repo.GetByIdAsync(id, x => new MenuItem { Id = x.Id, Image = x.Image }, false, false);
        if (menuItem == null) throw new NotFoundException<MenuItem>();
        string imagePath = Path.Combine(_env.WebRootPath, "imgs", "menuItem", menuItem.Image);
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
        _repo.Delete(menuItem);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<MenuItemGetDto>> GetAllAsync()
    {
        var meniItems = await _repo.GetAllAsync(x => new MenuItemGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Image = x.Image,
            Price = x.Price,
            MenuId = x.MenuId
        }, true, false);
        return meniItems;
    }

    public async Task<MenuItemGetDto> GetByIdAsync(int id)
    {
        var menuItem = await _repo.GetByIdAsync(id, x => new MenuItemGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Image = x.Image,
            Price = x.Price,
            MenuId = x.MenuId
        }, true, false);
        if (menuItem == null) throw new NotFoundException<MenuItem>();
        return menuItem;
    }

    public async Task UpdateAsync(int id, MenuItemUpdateDto dto)
    {
        if (dto.File != null)
        {
            if (!dto.File.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.File.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }
        var menuItem = await _repo.GetByIdAsync(id, x => new MenuItem
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Image = x.Image
        }, false, false);
        if (menuItem == null) throw new NotFoundException<MenuItem>();
        _mapper.Map(dto, menuItem);
        string imagePath = Path.Combine(_env.WebRootPath, "imgs", "menuItem", menuItem.Image);
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
        await _repo.SaveAsync();
    }
}
