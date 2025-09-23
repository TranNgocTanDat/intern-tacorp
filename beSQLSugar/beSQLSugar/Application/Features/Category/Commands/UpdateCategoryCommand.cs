using beSQLSugar.Application.DTO.request;
using beSQLSugar.Application.DTO.response;
using MediatR;

namespace beSQLSugar.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryResponse>
    {
        public int Id { get; set; }
        public CategoryRequest? Request { get; set; }
        public UpdateCategoryCommand(int id, CategoryRequest? request)
        {
            Id = id;
            Request = request;
        }
    }
}
