using AdvicerApp.BL.DTOs.CategoryDtos;
using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AutoMapper;

namespace AdvicerApp.BL.Services.Implements;

public class CategoryService(ICategoryRepository _repo, IMapper _mapper) : ICategoryService
{
    public async Task<int> CreateAsync(CategoryCreateDto dto)
    {
        if (await _repo.IsExistAsync(x => x.Name == dto.Name)) throw new ExistsException<Category>("Category is exist");
        var category = _mapper.Map<Category>(dto);
        await _repo.AddAsync(category);
        await _repo.SaveAsync();
        return category.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id, x => new Category { Id = id }, false, false);
        if (category is null) throw new NotFoundException<Category>("Category is not found");
        _repo.Delete(category);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<CategoryGetDto>> GetAllAsync()
    {
        var entity = await _repo.GetAllAsync(x => new CategoryGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Restaurants = x.Restaurants.Select(r => new RestaurantGetDto
            {
                Address = r.Address,
                Phone = r.Phone,
                Name = r.Name,
                CategoryName = r.Category.Name,
                Image = r.Image,
                Images = r.RestaurantImages.Select(img => img.ImageUrl).ToList(),
                Description = r.Description,
                OwnerId = r.OwnerId
            }).ToList()
        }
        , asNoTrack: true, isDeleted: false);
        return entity;
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id, x => new CategoryGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Restaurants = x.Restaurants.Select(r => new RestaurantGetDto
            {
                Address = r.Address,
                Phone = r.Phone,
                Name = r.Name,
                CategoryName = r.Category.Name,
                Image = r.Image,
                Images = r.RestaurantImages.Select(img => img.ImageUrl).ToList(),
                Description = r.Description,
                OwnerId = r.OwnerId
            }).ToList()
        }
        , asNoTrack: true, isDeleted: false);
        if (category == null) throw new NotFoundException<Category>("Category is not found");
        return category;
    }

    public async Task UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var category = await _repo.GetByIdAsync(id, x => new Category { Id = id }, false, false);
        if (category is null) throw new NotFoundException<Category>("Category is not found");
        _mapper.Map(dto, category);
        await _repo.SaveAsync();

    }
}
