using FluentValidation;
using MediatR;

namespace Yenilen.Application.Features.User.Commands;

public sealed class AddFavouriteStoreCommand:IRequest<int>
{
    public int UserId { get; set; }
    public int StoreId { get; set; }
    
}

public sealed class AddFavouriteStoreCommandValidator : AbstractValidator<AddFavouriteStoreCommand>
{
    public AddFavouriteStoreCommandValidator()
    {
        RuleFor(x => x.StoreId)
            .NotEmpty().WithMessage("StoreId atanmali.");
    }
}
