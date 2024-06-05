using Microsoft.EntityFrameworkCore;
using RSAllies.Venues.Data;

namespace RSAllies.Venues.Services
{
    internal static class DatabaseService
    {
        public static async Task<bool> IsVenueUniqueAsync(VenueDbContext context, string name)
        {
            return await context.Venues.AllAsync(v => v.Name != name);
        }
    }
}
