using AutoMapper;
using System.Collections.Generic;
using Entities = Recycling.Model.Entities;

namespace Recycling.API.Models.Mappings
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile()
        {
//            CreateMap<Entities.User, Models.User>()
//                .ForMember(vm => vm.UserHubs,
//                    map => map.MapFrom(u => u.UserHubs));

            CreateMap<Entities.Fraction, Fraction>()
                .ForMember(vm => vm.User,
                    map => map.MapFrom(u => u.User.FirstName + u.User.LastName))
                .ForMember(vm => vm.Hub,
                    map => map.MapFrom(u => u.Hub.Name));

            CreateMap<Entities.Hub, Hub>()
                .ForMember(vm => vm.WasteManagement,
                    map => map.MapFrom(u => u.WasteManagement.Name))
                .ForMember(vm => vm.TotalFractions,
                    map => map.UseValue(new List<Fraction>()));

            CreateMap<Entities.WasteManagement, WasteManagement>()
                .ForMember(vm => vm.Hubs,
                    map => map.UseValue(new List<Hub>()));
        }
    }
}