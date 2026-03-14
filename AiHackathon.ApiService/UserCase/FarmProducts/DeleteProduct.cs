using AiHackathon.ApiService.Interfaces;
using AiHackathon.ApiService.Validations;
using MediatR;

namespace AiHackathon.ApiService.UserCase.FarmProducts
{
    public static class DeleteProduct
    {
        public record Command(string Id): IRequest<HandlerResult<string>>;

        public sealed class Handler(IFarmProductsRepository repository): IRequestHandler<Command, HandlerResult<string>>
        {
            public async Task<HandlerResult<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await repository.DeleteAsync(request.Id);
                if(!result)
                {
                    var errorValue = ValidationError.Create("system", "An error occurred while deleting product.").Values;
                    return HandlerResult<string>.Failure(errorValue, "product deletion failed.");
                }
                return HandlerResult<string>.Success(request.Id, "product deleted successfully.");
            }
        }
    }
}
