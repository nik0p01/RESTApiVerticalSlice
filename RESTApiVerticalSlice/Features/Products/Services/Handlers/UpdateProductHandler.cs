using MediatR;
using RESTApiVerticalSlice.Features.Products.Data;
using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Services.Handlers;

public sealed class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; }
    public ProductDto Dto { get; }
    public UpdateProductCommand(Guid id, ProductDto dto) 
    {
        Id = id;
        Dto = dto; 
    }
}

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repository;
    public UpdateProductHandler(IProductRepository repo)
    {
        _repository = repo;
    }

    public Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Id, request.Dto.Name, request.Dto.Price);
        return _repository.UpdateAsync(product);
    }
}
