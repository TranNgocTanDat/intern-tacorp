using beSQLSugar.Application.Dto.request.Category;
using beSQLSugar.Application.Dto.response.Category;
using MediatR;
using System.Security.Claims;

namespace beSQLSugar.Application.Features.Category.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public CategoryRequest? Request { get; set; }
        public ClaimsPrincipal User { get; set; }
        public CreateCategoryCommand(CategoryRequest? request, ClaimsPrincipal user)
        {
            Request = request;
            User = user;
        }
    }
    
}
