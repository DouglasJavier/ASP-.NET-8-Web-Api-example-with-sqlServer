using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services
{
    public class ProductService : ICommonService<ProductGetDto, ProductInsertDto, ProductUpdateDto>
    {
        public IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public List<string> Errors => throw new NotImplementedException();

        public async Task<ProductGetDto> Add(ProductInsertDto productDTO)
        {
            var product = new Product()
            {
                Name = productDTO.Name,
                Model = productDTO.Model,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                BrandID = productDTO.BrandID,
            };
            await _productRepository.Add(product);

            var productGetDTO = new ProductGetDto()
            {
                Name = productDTO.Name,
                Model = productDTO.Model,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                BrandID = productDTO.BrandID,
            };
            await _productRepository.Save();
            return productGetDTO;
        }

        public async Task<ProductGetDto> Delete(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            var productGetDTO = new ProductGetDto()
            {
                Name = product.Name,
                Model = product.Model,
                Price = product.Price,
                Stock = product.Stock,
                BrandID = product.BrandID ?? 0,
            };
            _productRepository.Delete(product);
            await _productRepository.Save();
            return productGetDTO;
        }

        public async Task<IEnumerable<ProductGetDto>> Get()
        {
            var products = await _productRepository.Get();
            var productDtos = products.Select(p => new ProductGetDto
            {
                Id = p.Id,
                Name = p.Name,
                Model = p.Model,
                Stock = p.Stock,
                Price = p.Price,
                BrandID = p.BrandID ?? 0,
                //Brand = p.Brand
            });

            return productDtos;
        }

        public async Task<ProductGetDto> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            var productDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Model = product.Model,
                Price = product.Price,
                Stock = product.Stock,
                BrandID = product.BrandID ?? 0
            };

            return productDto;
        }

        public async Task<ProductGetDto> Update(int id, ProductUpdateDto productDTO)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            product.Name = productDTO.Name;
            product.Model = productDTO.Model;
            product.Price = productDTO.Price;
            product.Stock = productDTO.Stock;
            product.BrandID = productDTO.BrandID;
            _productRepository.Update(product);
            await _productRepository.Save();

            var respProductDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Model = product.Model,
                Price = product.Price,
                Stock = product.Stock,
                BrandID = product.BrandID ?? 0
            };
            return respProductDto;
        }
    }
}