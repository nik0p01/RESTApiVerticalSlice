using MediatR;

using RESTApiVerticalSlice.Storage;
using RESTApiVerticalSlice.Storage.Models;

namespace RESTApiVerticalSlice.Features.Products.Create;

public sealed class CreateProductCommand : IRequest<CreateProductResponseDto>
{
    public CreateProductRequestDto Dto { get; }
    public CreateProductCommand(CreateProductRequestDto dto)
    {
        Dto = dto;
    }
}

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponseDto>
{
    private readonly InMemoryProductStorage _storage;

    public CreateProductHandler(InMemoryProductStorage storage)
    {
        _storage = storage;
    }

    public async Task<CreateProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductEntity(Guid.NewGuid(), request.Dto.Name, request.Dto.Price);
        var created = await _storage.CreateAsync(product);
        return new CreateProductResponseDto(created.Id, created.Name, created.Price);
    }
}
