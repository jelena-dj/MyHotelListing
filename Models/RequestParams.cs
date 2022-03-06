using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.Models
{
	public class RequestParams
	{
		const int maxPageSize = 50;
		public int PageNumber { get; set; } = 1;  //default
		private int _pageSize = 10;		//default

		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}

	}
}
