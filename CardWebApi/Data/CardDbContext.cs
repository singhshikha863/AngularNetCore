using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CardWebApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CardWebApi.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions options ) : base(options)
        {
            
        }

        public DbSet<Cards> Cards { get; set; }
    }
}
