using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyHotelListing.Configurations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.Data
{
	public class DatabaseContext : IdentityDbContext<ApiUser>
	{
		public DatabaseContext(DbContextOptions options) : base(options)  //DbContext constructor variations
		{}

		public DbSet<Country> Countries { get; set; }

		public DbSet<Hotel> Hotels { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new CountryConfiguration());
			builder.ApplyConfiguration(new HotelConfiguration());
			builder.ApplyConfiguration(new RoleConfiguration());

		}

	}
}
