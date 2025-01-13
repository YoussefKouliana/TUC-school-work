using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Individuellt_programmeringsprojekt
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }


        public LibraryDbContext() : base("LibraryConnectionString")
        {
        }
    }
}
