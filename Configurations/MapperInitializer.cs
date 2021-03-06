using AutoMapper;
using MyHotelListing.Data;
using MyHotelListing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing.Configurations
{
	public class MapperInitializer	: Profile
	{
		public MapperInitializer()
		{
			CreateMap<Country, CountryDTO>().ReverseMap();
			CreateMap<Country, CreateCountryDTO>().ReverseMap();
			CreateMap<Country, UpdateCountryDTO>().ReverseMap();
			CreateMap<Hotel, HotelDTO>().ReverseMap();
			CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
			CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();
			CreateMap<ApiUser, UserDTO>().ReverseMap();
		}
	}
}
