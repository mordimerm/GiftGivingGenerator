using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		//Person
		CreateMap<Person, PersonDto>();
		CreateMap<Person, OrganizerToSendEmailDto>();

		//Event
		CreateMap<CreateEventDto, Event>();

		CreateMap<Event, OutputEventDto>();
		CreateMap<Event, EventWithPersonsDto>();
		CreateMap<Event, EventToListDto>();
		CreateMap<Event, EventToSendEmailDto>();

		//DrawingResult
		CreateMap<DrawingResult, DrawingResultDto>()
			.ForMember(x=>x.GiverName, y=>y.MapFrom(z=>z.GiverPerson.Name))
			.ForMember(x=>x.RecipientName, y=>y.MapFrom(z=>z.RecipientPerson.Name));
		CreateMap<DrawingResult, DrawingResultsForOrganizerDto>()
			.ForMember(x => x.GiverName, y => y.MapFrom(z => z.GiverPerson.Name));
		
		CreateMap<DrawingResult, DrawingResultForUserDto>()
			.ForMember(x => x.EventName, y => y.MapFrom(z => z.Event.Name))
			.ForMember(x => x.EndDate, y => y.MapFrom(z => z.Event.EndDate))
			.ForMember(x => x.Budget, y => y.MapFrom(z => z.Event.Budget))
			.ForMember(x => x.Message, y => y.MapFrom(z => z.Event.Message))
			.ForMember(x => x.GiverName, y => y.MapFrom(z => z.GiverPerson.Name))
			.ForMember(x => x.RecipientName, y => y.MapFrom(z => z.RecipientPerson.Name))
			.ForMember(x => x.RecipientGiftWishes, y => y.MapFrom(z => z.Event.GiftWishes.SingleOrDefault(za =>za.PersonId==z.RecipientPersonId).Wish));
	}
}