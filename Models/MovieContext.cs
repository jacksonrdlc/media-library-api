using Microsoft.EntityFrameworkCore;

namespace media_library_api.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<MovieItem> MovieItems { get; set; }

    }
}