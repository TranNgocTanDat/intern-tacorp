using beSQLSugar.Application.Dto.response.Category;
using beSQLSugar.Application.Features.Category.Commands;
using beSQLSugar.Application.Services.CategoryServices;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Handlers
{
    public class CategoryCommandHandler :
        IRequestHandler<CreateCategoryCommand, CategoryResponse>,
        IRequestHandler<UpdateCategoryCommand, CategoryResponse>,
        IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryService _service;
        public CategoryCommandHandler(ICategoryService service)
        {
            _service = service;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        { 
            return await _service.DeleteAsync(request.Id);
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.Request is null)
                throw new ArgumentNullException(nameof(request.Request));

            var result = await _service.UpdateAsync(request.Id, request.Request, request.User);
            if (result is null)
                throw new InvalidOperationException("Update failed: Category not found or update unsuccessful.");

            return result;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.AddAsync(request.Request!, request.User);
            if (result is null)
                throw new InvalidOperationException("Create failed: Category could not be created.");
            return result;
        }
    }
}

