using AutoMapper;
using CarRentalDDD.API.Cars;
using CarRentalDDD.API.Customers;
using CarRentalDDD.API.Rentals;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals;

namespace CarRentalDDD.API.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(x=>x.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(x => x.Address.City))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(x => x.Address.ZipCode))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(x => x.Phone.Value));

            CreateMap<Car, CarDTO>();
            CreateMap<Car, CarWithMaintenancesDTO>(); 

            CreateMap<Maintenance, MaintenanceDTO>();

            CreateMap<Rental, RentalDTO>();

        }
    }
}
