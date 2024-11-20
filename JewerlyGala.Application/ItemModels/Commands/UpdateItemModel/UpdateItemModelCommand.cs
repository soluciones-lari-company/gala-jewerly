using JewerlyGala.Domain.Exceptions;
using JewerlyGala.Domain.Repositories;
using MediatR;

namespace JewerlyGala.Application.ItemModels.Commands.UpdateItemModel
{
    public class UpdateItemModelCommand : IRequest
    {
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
    }

    public class UpdateItemModelCommandHandler(IItemModelRepository itemModelRepository) : IRequestHandler<UpdateItemModelCommand>
    {

        public async Task Handle(UpdateItemModelCommand request, CancellationToken cancellationToken)
        {
            var model = await itemModelRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Model not found");


            await itemModelRepository.UpdateAsync(request.Id, request.ModelName);
        }
    }
}
