using AutoMapper;
using Udlånssystem_API.Models;
using Udlånssystem_API.DTOs;

namespace Udlånssystem_API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Domain to DTO

            CreateMap<Bruger, BrugerDTO>()
                .ForMember(
                    dest => dest.BrugerGruppeID,
                    opt => opt.MapFrom(src => src.BrugerGruppe != null ? src.BrugerGruppe.BrugerGruppeID : 0)
                )
                .ForMember(
                    dest => dest.PostNrID,
                    opt => opt.MapFrom(src => src.Postnr != null ? src.Postnr.PostnrID : 0)
                )
                .ForMember(
                    dest => dest.StamKlasseID,
                    opt => opt.MapFrom(src => src.Stamklasse != null ? src.Stamklasse.StamklasseID : 0)
                );



            // DTO to Domain
            CreateMap<BrugerDTO, Bruger>();
        }
    }
}

