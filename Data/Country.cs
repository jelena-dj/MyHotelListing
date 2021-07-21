using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.Data
{
	public class Country
	{
		public int Id { get; set; }  //primary key

		public string Name { get; set; }

		public string ShortName { get; set; }
	}
}
