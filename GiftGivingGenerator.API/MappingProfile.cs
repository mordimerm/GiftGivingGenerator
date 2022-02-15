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
		
	}
}