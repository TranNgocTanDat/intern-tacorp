using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Application.Dto.response.Category;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.Category.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryResponse>
    {
        public int Id { get; set; }
        public CategoryRequest? Request { get; set; }
        public ClaimsPrincipal User { get; set; }
        public UpdateCategoryCommand(int id, CategoryRequest? request, ClaimsPrincipal user)
        {
            Id = id;
            Request = request;
            User = user;
        }
    }
}
