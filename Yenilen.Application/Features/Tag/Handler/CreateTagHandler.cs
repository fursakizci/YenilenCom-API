using AutoMapper;
using MediatR;
using TS.Result;
using Yenilen.Application.Features.Tag.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;

namespace Yenilen.Application.Features.Tag.Handler;

internal sealed class CreateTagHandler : IRequestHandler<CreateTagCommand, Result<CreateTagCommandResponse>>
{

    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTagHandler(IUnitOfWork unitOfWork, ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<CreateTagCommandResponse>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {

        var tag = _mapper.Map<Domain.Entities.Tag>(request);

        await _tagRepository.AddAsync(tag);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateTagCommandResponse()
        {
            tagId = tag.Id
        };

        return Result<CreateTagCommandResponse>.Succeed(response);
    }
}