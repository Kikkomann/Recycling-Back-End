using AutoMapper;

namespace Recycling.API.Models.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<EntityToModelMappingProfile>();
            });
        }
    }
}
