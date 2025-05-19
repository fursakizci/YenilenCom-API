using FluentValidation;
using MediatR;
using TS.Result;

namespace Yenilen.Application.Features.User.Commands;

public sealed class AddFavouriteStoreCommand:IRequest<Result<AddFavouriteStoreCommandResponse>>
{
    public int UserId { get; set; }
    public int StoreId { get; set; }
    
}

public sealed class AddFavouriteStoreCommandResponse
{
    public int FavouriteId { get; set; }
}

public sealed class AddFavouriteStoreCommandValidator : AbstractValidator<AddFavouriteStoreCommand>
{
    public AddFavouriteStoreCommandValidator()
    {
        RuleFor(x => x.StoreId)
            .NotEmpty().WithMessage("StoreId atanmali.");
    }
}
