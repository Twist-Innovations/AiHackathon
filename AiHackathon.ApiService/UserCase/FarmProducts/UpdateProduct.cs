using AiHackathon.ApiService.Interfaces;
using AiHackathon.ApiService.Validations;
using MediatR;


namespace AiHackathon.ApiService.UserCase.FarmProducts
{
    public static class UpdateProduct
    {
        public record Command(string Id, string FarmId, string Name, double Quantity): IRequest<HandlerResult<string>>;

        public sealed class Handler(IFarmProductsRepository repository): IRequestHandler<Command, HandlerResult<string>>
        {
            public async Task<HandlerResult<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await repository.GetAsync(request.Id);

                if(entity is null)
                {
                    var errorValue = ValidationError.Create("product", "product doesn't exist.").Values;
                    return HandlerResult<string>.Failure(errorValue, "product update failed.");
                }

                entity.Name = request.Name;

                entity.FarmId = request.FarmId
                    ;
                entity.Quantity = request.Quantity;

                var result = await repository.UpdateAsync(entity);

                if(!result)
                {
                    var errorValue = ValidationError.Create("system", "An error occurred while updating website product.").Values;
                    return HandlerResult<string>.Failure(errorValue, "product update failed.");
                }
                await repository.SaveAsync();
                return HandlerResult<string>.Success(entity.Id, "product updated successfully.");
            }
        }
    }
}
