using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Exclusion;
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
		CreateMap<CreateEventWithPersonsDto, Event>();

		CreateMap<Event, EventDto>()
			.ForMember(x=>x.OrganizerName, y=>y.MapFrom(z=>z.Organizer.Name));
		CreateMap<Event, EventToSendEmailDto>();
		
		//Exclusion
		CreateMap<Exclusion, ExclusionDto>()
			.ForMember(x=>x.Excluded, y=>y.MapFrom(z=>z.Excluded.Name));
			
		//DrawingResult
		CreateMap<DrawingResult, DrawingResultsForOrganizerDto>()
			.ForMember(x => x.GiverName, y => y.MapFrom(z => z.GiverPerson.Name));
		CreateMap<DrawingResult, DrawingResultForUserDto>()
			.ForMember(x => x.EventId, y => y.MapFrom(z => z.Event.Id))
			.ForMember(x => x.EventName, y => y.MapFrom(z => z.Event.Name))
			.ForMember(x => x.EndDate, y => y.MapFrom(z => z.Event.EndDate))
			.ForMember(x => x.Budget, y => y.MapFrom(z => z.Event.Budget))
			.ForMember(x => x.Message, y => y.MapFrom(z => z.Event.Message))
			.ForMember(x => x.GiverName, y => y.MapFrom(z => z.GiverPerson.Name))
			.ForMember(x => x.GiverGiftWishes, y => y.MapFrom(z => z.Event.GiftWishes.SingleOrDefault(za =>za.PersonId==z.GiverPersonId).Wish))
			.ForMember(x => x.RecipientName, y => y.MapFrom(z => z.RecipientPerson.Name))
			.ForMember(x => x.RecipientGiftWishes, y => y.MapFrom(z => z.Event.GiftWishes.SingleOrDefault(za =>za.PersonId==z.RecipientPersonId).Wish));
	}
}