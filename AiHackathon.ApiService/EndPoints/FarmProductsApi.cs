using AiHackathon.ApiService.ApiRequests;
using AiHackathon.ApiService.UserCase.FarmProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AiHackathon.ApiService.EndPoints
{
    public static class ApiEndPoints
    {
        public static void FarmProductApi(this IEndpointRouteBuilder group)
        {
            group.MapPost("/", Create);
            group.MapGet("/{id}", Get);
            group.MapGet("/", GetAll);
            group.MapPut("/", Update);
            group.MapDelete("/{id}", Delete);
        }

        private static async Task<IResult> Get([FromServices] IMediator mediator, [FromRoute] string id)
        {
            var result = await mediator.Send(new GetProductById.Query(id));

            if(result.IsSuccess)
                return Results.Ok(result.Result);

            return Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> GetAll([FromServices] IMediator mediator)
        {
            var result = await mediator.Send(new GetAllProducts.Query());

            if(result.IsSuccess)
                return Results.Ok(result.Result);

            return Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> Create([FromServices] IMediator mediator, CreateFramProductRequest request)
        {
            var result = await mediator.Send(new CreateProduct.Command(request.FarmId, request.Name, request.Quantity));

            if(result.IsSuccess)
                return Results.Ok(result.Result);

            return Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> Update([FromServices] IMediator mediator, UpdateFarmProductRequest request)
        {
            var result = await mediator.Send(new UpdateProduct.Command(request.Id, request.FarmId, request.Name, request.Quantity)
            );

            if(result.IsSuccess)
                return Results.Ok(result.Result);

            return Results.BadRequest(result.Errors);
        }

        private static async Task<IResult> Delete([FromServices] IMediator mediator, [FromRoute] string id)
        {
            var result = await mediator.Send(new DeleteProduct.Command(id));

            if(result.IsSuccess)
                return Results.Ok(result.Result);

            return Results.BadRequest(result.Errors);
        }


    }
}
