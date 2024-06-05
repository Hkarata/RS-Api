using Microsoft.EntityFrameworkCore;
using RSAllies.Users.Data;

namespace RSAllies.Users.Services;

public static class DatabaseService
{
    public static async Task<bool> CheckUserName(UsersDbContext context, string firstName, string middleName, string lastName, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .AnyAsync(u => u.FirstName == firstName && u.MiddleName == middleName && u.LastName == lastName, cancellationToken);
    }
    public static async Task<bool> IsUserUniqueAsync(UsersDbContext context, string email, string phone)
    {
        return await context.Users
            .AsNoTracking()
            .Include(u => u.Account)
            .AnyAsync(u => u.Account!.Email == email || u.Account.Phone == phone);
    }
}
