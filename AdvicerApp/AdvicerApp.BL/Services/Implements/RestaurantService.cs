using AdvicerApp.BL.DTOs.RestaurantDtos;
using AdvicerApp.BL.Exceptions.Common;
using AdvicerApp.BL.Extensions;
using AdvicerApp.BL.ExternalServices.Interfaces;
using AdvicerApp.BL.Services.Interface;
using AdvicerApp.Core.Entities;
using AdvicerApp.Core.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace AdvicerApp.BL.Services.Implements;

public class RestaurantService(IRestaurantRepository _repo, IMapper _mapper, ICategoryRepository _catRepo, IWebHostEnvironment _env, ICurrentUser _user) : IRestaurantService
{
    private string _userId = _user.GetId();

    public async Task<int> CreateAsync(CreateRestaurantDto dto)
    {
        if (dto.File != null)
        {
            if (!dto.File.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.File.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }
        if (dto.Files.Any())
        {
            if (!dto.Files.All(x => x.IsValidType("image"))) throw new InvalidFileException("The files must be images");
            if (!dto.Files.All(x => x.IsValidSize(10))) throw new InvalidFileException("The files can be max 10 MB.");
        }
        var restaurant = _mapper.Map<Restaurant>(dto);
        restaurant.OwnerId = _userId;
        if (!await _catRepo.IsExistAsync(dto.CategoryId)) throw new NotFoundException<Category>("Category is not found");
        restaurant.Image = await dto.File!.UploadAsync(_env.WebRootPath, "imgs", "restaurant");
        restaurant.RestaurantImages = dto.Files.Select(x => new RestaurantImage
        {
            ImageUrl = x.UploadAsync(_env.WebRootPath, "imgs", "restaurant").Result
        }).ToList();
        await _repo.AddAsync(restaurant);
        await _repo.SaveAsync();
        return restaurant.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var restaurant = await _repo.GetByIdAsync(id, x => new Restaurant { Id = id,
            Image = x.Image,
            RestaurantImages = x.RestaurantImages
        }, false, false);
        if (restaurant == null) throw new NotFoundException<Restaurant>("Restaurant is not found");
        string imagePath = Path.Combine(_env.WebRootPath, "imgs", "restaurant", restaurant.Image);
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
        foreach (var image in restaurant.RestaurantImages)
        {
            string imagePaths = Path.Combine(_env.WebRootPath, "imgs", "restaurant", image.ImageUrl);
            if (System.IO.File.Exists(imagePaths))
            {
                System.IO.File.Delete(imagePaths);
            }
        }
        _repo.Delete(restaurant);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<RestaurantGetDto>> GetAllAsync()
    {
        var restaurants = await _repo.GetAllAsync(x => new RestaurantGetDto
        {
            Address = x.Address,
            Phone = x.Phone,
            Name = x.Name,
            CategoryName = x.Category.Name,
            Image = x.Image,
            Images = x.RestaurantImages.Select(img => img.ImageUrl).ToList(),
            Description = x.Description,
            AverageRating = Convert.ToDecimal(x.Ratings.Any() ? x.Ratings.Average(r=>r.Score) : 0 ),
            OwnerId = x.OwnerId
        },asNoTrack:true,isDeleted:false);

        return restaurants;
    }

    public async Task<RestaurantGetDto> GetByIdAsync(int id)
    {
        var restaurant = await _repo.GetByIdAsync(id, x => new RestaurantGetDto
        {
            Address = x.Address,
            Phone = x.Phone,
            Name = x.Name,
            CategoryName = x.Category.Name,
            Image = x.Image,
            Images = x.RestaurantImages.Select(img => img.ImageUrl).ToList(),
            Description = x.Description,
            OwnerId = x.OwnerId
        }, asNoTrack: true, isDeleted: false);
        if (restaurant == null) throw new NotFoundException<Restaurant>("Restaurant not found");
        return restaurant;
        
    }

    public async Task UpdateAsync(int id, UpdateResturantDto dto)
    {
        if (dto.File != null)
        {
            if (!dto.File.IsValidType("image")) throw new InvalidFileException("The file must be image");
            if (!dto.File.IsValidSize(3)) throw new InvalidFileException("The file can be max 5 MB.");
        }
        if (dto.Files.Any())
        {
            if (!dto.Files.All(x => x.IsValidType("image"))) throw new InvalidFileException("The files must be images");
            if (!dto.Files.All(x => x.IsValidSize(10))) throw new InvalidFileException("The files can be max 10 MB.");
        }
        var restaurant = await _repo.GetByIdAsync
            (id, x => new Restaurant
            {
                Id = id,
                Image = x.Image,
                RestaurantImages = x.RestaurantImages
            }, false, false);
        if (!await _catRepo.IsExistAsync(dto.CategoryId)) throw new NotFoundException<Category>("Category is not found");
        _mapper.Map(dto, restaurant);
        if (restaurant == null) throw new NotFoundException<Restaurant>("Restaurant is not found");
        string imagePath = Path.Combine(_env.WebRootPath, "imgs", "restaurant", restaurant.Image);
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
        foreach (var image in restaurant.RestaurantImages)
        {
            string imagePaths = Path.Combine(_env.WebRootPath, "imgs", "restaurant", image.ImageUrl);
            if (System.IO.File.Exists(imagePaths))
            {
                System.IO.File.Delete(imagePaths);
            }
        }
        restaurant.Image = await dto.File!.UploadAsync(_env.WebRootPath, "imgs", "restaurant");
        var restaurantImages = new List<RestaurantImage>();

        foreach (var file in dto.Files)
        {
            if (file != null)
            {

                var imageUrl = await file.UploadAsync(_env.WebRootPath, "imgs", "restaurant");
                restaurantImages.Add(new RestaurantImage { ImageUrl = imageUrl });
            }

        }
        await _repo.SaveAsync();
    }
}
