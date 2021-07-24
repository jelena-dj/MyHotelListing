using MyHotelListing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.IRepository
{
	public interface IUnitOfWork  : IDisposable
	{
		IGenericRepository<Country> Contries { get; }
		IGenericRepository<Hotel> Hotels { get; }
		Task Save();
	}
}
