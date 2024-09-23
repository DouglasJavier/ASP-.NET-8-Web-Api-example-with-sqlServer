using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services
{
    public class SaleService : ICommonService<SaleGetDto, SaleInsertUpdateDto, SaleInsertUpdateDto>
    {
        public IRepository<Sale> _saleRepository;
        public IRepository<Product> _productRepository;
        public List<string> Errors { get; }

        public SaleService(IRepository<Sale> SaleRepository, IRepository<Product> productRepository)
        {
            _saleRepository = SaleRepository;
            _productRepository = productRepository;
            Errors = new List<string>();
        }

        public async Task<SaleGetDto> Add(SaleInsertUpdateDto saleDto)
        {
            var sale = new Sale
            {
                Date = saleDto.Date,
                ClientFistName = saleDto.ClientFistName,
                ClientLastName = saleDto.ClientLastName,
                ClientIDDocument = saleDto.ClientIDDocument,
                Carts = new List<Cart>()
            };

            foreach (var cartDto in saleDto.Carts)
            {
                var product = await _productRepository.GetById(cartDto.ProductID);
                if (product == null)
                {
                    Errors.Add($"Product with ID {cartDto.ProductID} not found.");
                    continue;
                }
                if (product.Stock < cartDto.Quantity)
                {
                    Errors.Add($"Not enough stock for product {product.Name}. Available: {product.Stock}, Requested: {cartDto.Quantity}.");
                    continue;
                }
                sale.Carts.Add(new Cart
                {
                    ProductID = cartDto.ProductID,
                    Quantity = cartDto.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = cartDto.Quantity * product.Price,
                    Product = product
                });
            }

            // Guardar la venta
            await _saleRepository.Add(sale);
            await _saleRepository.Save();

            // Retornar el DTO de respuesta
            return new SaleGetDto
            {
                Id = sale.Id,
                Date = sale.Date.ToString("yyyy-MM-dd"),
                ClientFistName = sale.ClientFistName,
                ClientLastName = sale.ClientLastName,
                ClientIDDocument = sale.ClientIDDocument,
                Carts = sale.Carts.Select(cart => new CartDto
                {
                    ProductID = cart.ProductID,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice,
                    TotalPrice = cart.TotalPrice,
                }).ToList()
            };
        }


        public async Task<SaleGetDto> Delete(int id)
        {
            var sale = await _saleRepository.GetById(id);
            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found.");
            }

            _saleRepository.Delete(sale);
            await _saleRepository.Save();

            return new SaleGetDto
            {
                Id = sale.Id,
                Date = sale.Date.ToString("yyyy-MM-dd"),
                ClientFistName = sale.ClientFistName,
                ClientLastName = sale.ClientLastName,
                ClientIDDocument = sale.ClientIDDocument
            };
        }

        public async Task<IEnumerable<SaleGetDto>> Get()
        {
            var Sales = await _saleRepository.Get();
            Console.WriteLine("########## Entro aqui");
            foreach (var sale in Sales)
            {
                Console.WriteLine($"{sale.Id}");
                // Si hay otros campos que quieras mostrar, agrégalos aquí
            }
            Console.WriteLine(Sales);
            var SaleDtos = Sales.Select(sale => new SaleGetDto
            {
                Id = sale.Id,
                Date = sale.Date + "",
                ClientFistName = sale.ClientFistName,
                ClientLastName = sale.ClientLastName,
                ClientIDDocument = sale.ClientIDDocument,
                Carts = sale.Carts.Select(cart => new CartDto
                {
                    ProductID = cart.ProductID,
                    Quantity = cart.Quantity,
                    SaleID = cart.SaleID,
                    TotalPrice = cart.TotalPrice,
                    UnitPrice = cart.UnitPrice
                })
                //Sale = p.Sale
            });

            return SaleDtos;
        }

        public async Task<SaleGetDto> GetById(int id)
        {
            var sale = await _saleRepository.GetById(id);
            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found.");
            }

            return new SaleGetDto
            {
                Id = sale.Id,
                Date = sale.Date.ToString("yyyy-MM-dd"),
                ClientFistName = sale.ClientFistName,
                ClientLastName = sale.ClientLastName,
                Carts = sale.Carts.Select(cart => new CartDto
                {
                    ProductID = cart.ProductID,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice,
                    TotalPrice = cart.TotalPrice
                }).ToList()
            };
        }

        public async Task<SaleGetDto> Update(int id, SaleInsertUpdateDto saleDTO)
        {
            var sale = await _saleRepository.GetById(id);

            if (sale == null)
            {
                throw new KeyNotFoundException("Sale not found.");
            }

            sale.Date = saleDTO.Date;
            sale.ClientFistName = saleDTO.ClientFistName;
            sale.ClientLastName = saleDTO.ClientLastName;
            sale.ClientIDDocument = saleDTO.ClientIDDocument;

            var cartsUpdate = new List<Cart>();

            foreach (var cartDto in saleDTO.Carts)
            {
                var product = await _productRepository.GetById(cartDto.ProductID);
                if (product == null)
                {
                    Errors.Add($"Product with ID {cartDto.ProductID} not found.");
                    continue;
                }

                cartsUpdate.Add(new Cart
                {
                    ProductID = cartDto.ProductID,
                    Quantity = cartDto.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = cartDto.Quantity * product.Price,
                    Product = product
                });
            }

            sale.Carts.Clear(); 
            sale.Carts = cartsUpdate; 

            _saleRepository.Update(sale);
            await _saleRepository.Save();

            var respSaleDto = new SaleGetDto
            {
                Id = sale.Id,
                Date = sale.Date.ToString("yyyy-MM-dd"),
                ClientFistName = sale.ClientFistName,
                ClientLastName = sale.ClientLastName,
                ClientIDDocument = sale.ClientIDDocument,
                Carts = sale.Carts.Select(cart => new CartDto
                {
                    ProductID = cart.ProductID,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.UnitPrice,
                    TotalPrice = cart.TotalPrice,
                }).ToList()
            };

            return respSaleDto;
        }

    }
}