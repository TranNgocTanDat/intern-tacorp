using beSQLSugar.Application.Dto.response.Category;
using beSQLSugar.Application.Features.Category.Queries;
using beSQLSugar.Application.Services.CategoryServices;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Handlers
{
    public class CategoryQueryHandler : 
        IRequestHandler<GetAllCategoryQuery, List<CategoryResponse>>,
        IRequestHandler<GetCategoryByIdQuery, CategoryResponse?>,
        IRequestHandler<FilterCategoryQuery, List<CategoryResponse>>,
        IRequestHandler<GetAllWithDetailsQuery, List<CategoryResponse>>,
        IRequestHandler<GetCategoryWithDetailsQuery, CategoryResponse?>,
        IRequestHandler<GetCategoryChildrenQuery, List<CategoryResponse>>
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

        public async Task<List<CategoryResponse>> Handle(GetAllWithDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllWithDetailsAsync();
        }

        public async Task<CategoryResponse?> Handle(GetCategoryWithDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCategoryWithDetailsAsync(request.Id);
        }

        public async Task<List<CategoryResponse>> Handle(GetCategoryChildrenQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCategoryChildrenAsync();
        }
    }
}
