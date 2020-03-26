using AutoMapper;
using CarRentalDDD.API.Mappings;

namespace CarRentalDDD.API.Tests
{
    public static class APITestsHelper
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });
            return new Mapper(config);
        }
    }
}
