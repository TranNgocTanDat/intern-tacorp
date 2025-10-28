using beSQLSugar.Application.Dto.request.Contact;
using beSQLSugar.Infrastructure.Database;
using beSQLSugar.Infrastructure.Database.Enities;

namespace beSQLSugar.Infrastructure.Repositories.Contacts
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(SqlSugarDbContext context) : base(context)
        {
        }

        public async Task<List<Contact>> FilterContactAsync(ContactFilterRequest request)
        {

            var query = _context.Db.Queryable<Contact>().Includes(c => c.Product); ;
            if (!string.IsNullOrEmpty(request.Fullname))
            {
                query = query.Where(c => c.Fullname.Contains(request.Fullname));
            }
            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(c => c.Email != null && c.Email.Contains(request.Email));
            }
            if (!string.IsNullOrEmpty(request.Phone))
            {
                query = query.Where(c => c.Phone != null && c.Phone.Contains(request.Phone));
            }
            if (!string.IsNullOrEmpty(request.Address))
            {
                query = query.Where(c => c.Address != null && c.Address.Contains(request.Address));
            }
            if (!string.IsNullOrEmpty(request.UserNote))
            {
                query = query.Where(c => c.UserNote != null && c.UserNote.Contains(request.UserNote));

            }
            if (request.ProductId.HasValue)
            {
                query = query.Where(c => c.ProductId == request.ProductId.Value);
            }
            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(c => c.Product != null && c.Product.ProductName != null && c.Product.ProductName.Contains(request.ProductName));
            }
            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(c => c.Status == request.Status);
            }
            if (request.FromUpdateTime.HasValue)
            {
                query = query.Where(c => c.UpdateTime >= request.FromUpdateTime.Value);
            }
            if (request.ToUpdateTime.HasValue)
            {
                query = query.Where(c => c.UpdateTime <= request.ToUpdateTime.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<List<Contact>> GetAllWithProductAsync()
        {
            return await _context.Db.Queryable<Contact>().Includes(c => c.Product).ToListAsync();
        }

        public async Task<Contact?> GetByIdWithProductAsync(int id)
        {
            return await _context.Db.Queryable<Contact>()
                                    .Includes(c => c.Product)
                                    .Where(c => c.Id == id)
                                    .FirstAsync();
        }

        public async Task<List<Contact>> GetByStatusAsync(string status)
        {
            return await _context.Db.Queryable<Contact>()
                                    .Includes(c => c.Product)
                                    .Where(c => c.Status == status)
                                    .ToListAsync();
        }


        public async Task<Contact> UpdateStatusAsync(int id, UpdateContactStatusRequest request)
        {
            var update = _context.Db.Updateable<Contact>()
                .SetColumns(c => c.Status == request.Status)
                .SetColumns(c => c.UpdateTime == DateTime.UtcNow);

            if (!string.IsNullOrEmpty(request.AdminNote))
            {
                update = update.SetColumns(c => c.AdminNote == request.AdminNote);
            }

            update = update.Where(c => c.Id == id);

            var result = await update.ExecuteReturnEntityAsync();
            if (result == null)
            {
                throw new InvalidOperationException($"Contact with Id {id} not found or update failed.");
            }
            return result;
        }


    }
}

