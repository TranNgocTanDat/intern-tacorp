using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public CategoryRequest? Request { get; set; }
        public CreateCategoryCommand(CategoryRequest? request)
        {
            Request = request;
        }
    }
    
}
