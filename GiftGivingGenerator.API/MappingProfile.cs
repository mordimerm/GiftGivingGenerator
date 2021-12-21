using AutoMapper;
using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Person, PersonDto>();
		CreateMap<Event, OutputEventDto>();
		CreateMap<InputEventDto, Event>();
		CreateMap<Event, EventWithPersonsDto>();
		CreateMap<OrganizerDto, Organizer>();
		CreateMap<Organizer, Person>()
			.ForMember(x => x.Id, x => x.Ignore())
			.ForMember(x=>x.OrganizerId, y=>y.MapFrom(z=>z.Id));
	}
}