using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DbContextes
{
    public class LibraryDB :DbContext
    {
public DbSet<Book> Books { get; set; }
public DbSet<Borrow> Borrows { get; set; }
public DbSet<Member> Members{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data source=DBFiles\librarydb.sqlite");
        }

        internal object MembersFirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}