using AiHackathon.ApiService.Interfaces;
using AiHackathon.ApiService.UserCase.FarmProducts.DTOs;
using AiHackathon.ApiService.Validations;
using MediatR;

namespace AiHackathon.ApiService.UserCase.FarmProducts
{
    public static class GetProductById
    {
        public record Query(string Id): IRequest<HandlerResult<ProductDto>>;

        public sealed class Handler(IFarmProductsRepository context): IRequestHandler<Query, HandlerResult<ProductDto>>
        {
            public async Task<HandlerResult<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await context.GetAsync(request.Id);

                if(product == null)
                {
                    var errorValue = ValidationError.Create("system", "An error occur while creating your product.").Values;

                    return HandlerResult<ProductDto>.Failure(errorValue, "Product creation fail.");
                }

                await context.SaveAsync();

                return HandlerResult<ProductDto>.Success(new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                }, "Product Created Successfully.");
            }
        }
    }
}
