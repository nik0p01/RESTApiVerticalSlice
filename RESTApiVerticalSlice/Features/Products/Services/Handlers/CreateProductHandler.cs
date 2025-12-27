using MediatR;
using RESTApiVerticalSlice.Features.Products.Data;
using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Services.Handlers;

public sealed class CreateProductCommand : IRequest<Product>
{
    public ProductDto Dto { get; }
    public CreateProductCommand(ProductDto dto)
    {
        Dto = dto;
    }
}

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repo)
    {
        _repository = repo;
    }

    public Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(Guid.NewGuid(), request.Dto.Name, request.Dto.Price);
        return _repository.CreateAsync(product);
    }
}
