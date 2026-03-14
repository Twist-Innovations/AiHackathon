using AiHackathon.ApiService.Interfaces;
using AiHackathon.ApiService.Models;
using AiHackathon.ApiService.Validations;
using MediatR;


namespace AiHackathon.ApiService.UserCase.FarmProducts
{
    public static class CreateProduct
    {
        public record Command(string FarmId, string Name, double Quantity): IRequest<HandlerResult<string>>;

        public sealed class Handler(IFarmProductsRepository repository): IRequestHandler<Command, HandlerResult<string>>
        {
            public async Task<HandlerResult<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new FarmProduct()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    FarmId = request.FarmId,
                    Quantity = request.Quantity
                };

                var result = await repository.AddAsync(entity);

                if(!result)
                {
                    var errorValue = ValidationError.Create("system", "An error occurred while creating website product.").Values;
                    return HandlerResult<string>.Failure(errorValue, "Website product creation failed.");
                }
                return HandlerResult<string>.Success(entity.Id, "Website product created successfully.");
            }
        }
    }
}
