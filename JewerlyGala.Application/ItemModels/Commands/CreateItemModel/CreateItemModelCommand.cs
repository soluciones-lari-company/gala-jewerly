
using JewerlyGala.Domain.Repositories;
using MediatR;


namespace JewerlyGala.Application.ItemModels.Commands.CreateItemModel
{
    public class CreateItemModelCommand: IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateItemModelCommandHandler : IRequestHandler<CreateItemModelCommand, int>
    {
        //private ILogger logger;
        private IItemModelsRepository itemModelsRepository;
        public CreateItemModelCommandHandler( IItemModelsRepository itemModelsRepository )
        {
            //this.logger = logger;
            this.itemModelsRepository = itemModelsRepository;
        }

        public async Task<int> Handle(CreateItemModelCommand request, CancellationToken cancellationToken)
        {
            var newItemModel = await itemModelsRepository.CreateAsync(request.Name);

            return newItemModel;
        }
    }
}
