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
                           .ForMember(dest => dest.BrugerGruppeID, opt => opt.MapFrom(src => src.BrugerGruppe != null ? src.BrugerGruppe.BrugerGruppeID : 0))
                           .ForMember(dest => dest.PostNrID, opt => opt.MapFrom(src => src.Postnr != null ? src.Postnr.PostnrID : 0))
                           .ForMember(dest => dest.StamKlasseID, opt => opt.MapFrom(src => src.Stamklasse != null ? src.Stamklasse.StamklasseID : 0))
                           .ForMember(dest => dest.BrugerGruppeNavn, opt => opt.MapFrom(src => src.BrugerGruppe != null ? src.BrugerGruppe.GruppeNavn : null))
                           .ForMember(dest => dest.StamKlasseNavn, opt => opt.MapFrom(src => src.Stamklasse != null ? src.Stamklasse.KlasseNavn : null));


            CreateMap<Bruger, LoginResponseDTO>()
                           .ForMember(dest => dest.BrugerGruppeNavn, opt => opt.MapFrom(src => src.BrugerGruppe != null ? src.BrugerGruppe.GruppeNavn : null))
                           .ForMember(dest => dest.PostNrID, opt => opt.MapFrom(src => src.Postnr != null ? src.Postnr.PostnrID : 0))
                           .ForMember(dest => dest.StamKlasseNavn, opt => opt.MapFrom(src => src.Stamklasse != null ? src.Stamklasse.KlasseNavn : null));

            CreateMap<Computer, ComputerDetailsDTO>()
                           .ForMember(dest => dest.MusType, opt => opt.MapFrom(src => src.MusModel != null ? src.MusModel.MusType : null))
                           .ForMember(dest => dest.FabrikatNavn, opt => opt.MapFrom(src => src.ComputerModel.Fabrikat != null ? src.ComputerModel.Fabrikat.FabrikatNavn : null))
                           .ForMember(dest => dest.ModelNavn, opt => opt.MapFrom(src => src.ComputerModel != null ? src.ComputerModel.ModelNavn : null));

            CreateMap<Udlån, RentedComputerDetailsDTO>() // Mapping from the loan entity because it includes all necessary details
                            .ForMember(dest => dest.UdlånID, opt => opt.MapFrom(src => src.UdlånID))
                            .ForMember(dest => dest.ComputerID, opt => opt.MapFrom(src => src.ComputerID))
                           .ForMember(dest => dest.MusType, opt => opt.MapFrom(src => src.Computer.MusModel != null ? src.Computer.MusModel.MusType : null))
                           .ForMember(dest => dest.FabrikatNavn, opt => opt.MapFrom(src => src.Computer.ComputerModel.Fabrikat != null ? src.Computer.ComputerModel.Fabrikat.FabrikatNavn : null))
                           .ForMember(dest => dest.ModelNavn, opt => opt.MapFrom(src => src.Computer.ComputerModel != null ? src.Computer.ComputerModel : null))
                            .ForMember(dest => dest.RegistreringsNummer, opt => opt.MapFrom(src => src.Computer.RegistreringsNummer))
                            .ForMember(dest => dest.Udlånsdato, opt => opt.MapFrom(src => src.Udlånsdato))
                            .ForMember(dest => dest.Udløbsdato, opt => opt.MapFrom(src => src.Udløbsdato))
                            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            // DTO to Domain
            CreateMap<BrugerDTO, Bruger>();

            CreateMap<OpretBrugerDTO, Bruger>();

            CreateMap<ReserveComputerRequestDTO, Udlån>();
        }
    }
}