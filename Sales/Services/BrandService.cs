using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services
{
    public class BrandService : ICommonService<BrandGetDto, BrandInsertDto, BrandUpdateDto>
    {
        public IRepository<Brand> _BrandRepository;

        public BrandService(IRepository<Brand> BrandRepository)
        {
            _BrandRepository = BrandRepository;
        }
        public List<string> Errors => throw new NotImplementedException();

        public async Task<BrandGetDto> Add(BrandInsertDto BrandDTO)
        {
            var Brand = new Brand()
            {
                Name = BrandDTO.Name,
                Acronym = BrandDTO.Acronym
            };
            await _BrandRepository.Add(Brand);

            var BrandGetDTO = new BrandGetDto()
            {
                Name = BrandDTO.Name,
                Acronym = BrandDTO.Acronym
            };
            await _BrandRepository.Save();
            return BrandGetDTO;
        }

        public async Task<BrandGetDto> Delete(int id)
        {
            var Brand = await _BrandRepository.GetById(id);

            if (Brand == null)
            {
                throw new KeyNotFoundException("Brand not found.");
            }

            var BrandGetDTO = new BrandGetDto()
            {
                Name = Brand.Name,
                Acronym = Brand.Acronym
            };
            _BrandRepository.Delete(Brand);
            await _BrandRepository.Save();
            return BrandGetDTO;
        }

        public async Task<IEnumerable<BrandGetDto>> Get()
        {
            var Brands = await _BrandRepository.Get();
            var BrandDtos = Brands.Select(b => new BrandGetDto
            {
                Id = b.Id,
                Name = b.Name,
                Acronym = b.Acronym
                //Brand = p.Brand
            });

            return BrandDtos;
        }

        public async Task<BrandGetDto> GetById(int id)
        {
            var Brand = await _BrandRepository.GetById(id);
            if (Brand == null)
            {
                throw new KeyNotFoundException("Brand not found.");
            }
            var BrandDto = new BrandGetDto
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Acronym = Brand.Acronym
            };

            return BrandDto;
        }

        public async Task<BrandGetDto> Update(int id, BrandUpdateDto BrandDTO)
        {
            var Brand = await _BrandRepository.GetById(id);

            if (Brand == null)
            {
                throw new KeyNotFoundException("Brand not found.");
            }

            Brand.Name = BrandDTO.Name;
            Brand.Acronym = BrandDTO.Acronym;
            _BrandRepository.Update(Brand);
            await _BrandRepository.Save();

            var respBrandDto = new BrandGetDto
            {
                Id = Brand.Id,
                Name = Brand.Name,
                Acronym = Brand.Acronym
            };
            return respBrandDto;
        }
    }
}