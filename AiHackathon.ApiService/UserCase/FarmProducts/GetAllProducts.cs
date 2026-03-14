using AiHackathon.ApiService.Interfaces;
using AiHackathon.ApiService.UserCase.FarmProducts.DTOs;
using AiHackathon.ApiService.Validations;
using MediatR;

namespace AiHackathon.ApiService.UserCase.FarmProducts
{
    public static class GetAllProducts
    {
        public record Query: IRequest<HandlerResult<List<ProductDto>>>;

        public sealed class Handler(IFarmProductsRepository context): IRequestHandler<Query, HandlerResult<List<ProductDto>>>
        {
            public async Task<HandlerResult<List<ProductDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await context.GetAsync();

                if(products.Count <= 0)
                {
                    return HandlerResult<List<ProductDto>>.Success([], "No products avaliable.");
                }

                var dto = products.Select(p => new ProductDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    FarmId = p.FarmId,
                    Quantity = p.Quantity
                }).ToList();

                await context.SaveAsync();

                return HandlerResult<List<ProductDto>>.Success(dto, "Products loaded Successfully.");
            }
        }
    }
}
