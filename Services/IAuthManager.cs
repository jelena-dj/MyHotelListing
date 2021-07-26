using MyHotelListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.Services
{
	public interface IAuthManager
	{
		Task<bool> ValidateUser(LoginUserDTO userDTO);
		Task<string> CreateToken();
	}
}
