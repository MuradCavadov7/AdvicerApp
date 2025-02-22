using AdvicerApp.BL.DTOs.MenuDtos;
using AdvicerApp.BL.DTOs.MenuItemDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AutoMapper;

namespace AdvicerApp.BL.Services.Implements;

public class MenuService(IMenuRepository _repo, IRestaurantRepository _restRepo, IMapper _mapper) : IMenuService
{
    public async Task<int> CreateAsync(MenuCreateDto dto)
    {
        if (!await _restRepo.IsExistAsync(dto.RestaurantId)) throw new NotFoundException<Restaurant>();
        var menu = _mapper.Map<Menu>(dto);
        await _repo.AddAsync(menu);
        await _repo.SaveAsync();
        return menu.Id;

    }

    public async Task DeleteAsync(int id)
    {
        var menu = await _repo.GetByIdAsync(id, x => new Menu { Id = x.Id }, false, false);
        if (menu is null) throw new NotFoundException<Menu>();
        await _repo.DeleteAndSaveAsync(id);
    }

    public async Task<IEnumerable<MenuGetDto>> GetAllAsync()
    {
        var menus = await _repo.GetAllAsync(x => new MenuGetDto
        {
            Id = x.Id,
            RestaurantId = x.RestaurantId,
            Name = x.Name,
            Description = x.Description,
            MenuItems = x.MenuItems.Select(m => new MenuItemGetDto
            {
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Image = m.Image
            }).ToList()
        }, true, false);
        return menus;
    }

    public async Task<MenuGetDto> GetByIdAsync(int id)
    {
        var menu = await _repo.GetByIdAsync(id, x => new MenuGetDto
        {
            Id = x.Id,
            RestaurantId = x.RestaurantId,
            Name = x.Name,
            Description = x.Description,
            MenuItems = x.MenuItems.Select(m => new MenuItemGetDto
            {
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                Image = m.Image
            }).ToList()
        }, true, false);
        if (menu is null) throw new NotFoundException<Menu>();
        return menu;
    }

    public async Task UpdateAsync(int id, MenuUpdateDto dto)
    {
        var menu = await _repo.GetByIdAsync(id, x => x, false, false);
        if (menu is null) throw new NotFoundException<Menu>();
        _mapper.Map(dto, menu);
        await _repo.SaveAsync();
    }
}
