using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OttoFlights.Models;

namespace OttoFlights.Data
{
    public class VolContext : DbContext
    {
        public VolContext (DbContextOptions<VolContext> options)
            : base(options)
        {
        }

        public DbSet<OttoFlights.Models.Vol> Vol { get; set; } = default!;
        public DbSet<OttoFlights.Models.Evenement> Evenement { get; set; } = default!;
        public DbSet<OttoFlights.Models.Utilisateur> Utilisateur { get; set; } = default!;
    }
}
