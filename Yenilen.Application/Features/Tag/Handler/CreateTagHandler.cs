using AutoMapper;
using MediatR;
using Yenilen.Application.Features.Tag.Commands;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services;

namespace Yenilen.Application.Features.Tag.Handler;

internal sealed class CreateTagHandler : IRequestHandler<CreateTagCommand, int>
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
    
    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {

        var tag = _mapper.Map<Domain.Entities.Tag>(request);

        await _tagRepository.AddAsync(tag);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return tag.Id;
    }
}