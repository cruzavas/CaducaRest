using CaducaRest.Models.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Models
{
	public class CaducaContext : DbContext
	{
		public CaducaContext(DbContextOptions<CaducaContext> options) : base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
	}
}
