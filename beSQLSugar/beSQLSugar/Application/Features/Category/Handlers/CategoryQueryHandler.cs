using beSQLSugar.Application.DTO.response;
using beSQLSugar.Application.Features.Category.Queries;
using beSQLSugar.Application.ServiceInterfaces;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Handlers
{
    public class CategoryQueryHandler : 
        IRequestHandler<GetAllCategoryQuery, List<CategoryResponse>>,
        IRequestHandler<GetCategoryByIdQuery, CategoryResponse?>,
        IRequestHandler<FilterCategoryQuery, List<CategoryResponse>>
    {
        private readonly ICategoryService _service;
        public CategoryQueryHandler(ICategoryService service)
        {
            _service = service;
        }

        public async Task<List<CategoryResponse>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync();
        }

        public async Task<CategoryResponse?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.Id);
        }

        public async Task<List<CategoryResponse>> Handle(FilterCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.FilterAsync(request.FilterRequest!);
        }
    }
}
