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
		CreateMap<Event, EventDto>();
		CreateMap<EventDto, Event>();
		CreateMap<Event, EventWithPersonsDto>();
	}
}
//
// {
// "id" : "56257D31-B958-4F12-A77D-08D9BFCCCCF9",
// "ids" :
// [
// 	"8CF19470-628E-4DC9-AF32-08D9BF3DB6CC",
// 	"9CF19470-628E-4DC9-AF32-08D9BF3DB6CC",
// 	"F0E8EA17-0BA7-41F5-0995-08D9BFBA717A",
// 	"6E00135E-86E1-40F6-0996-08D9BFBA717A",
// 	"31AFE4D0-3E8D-45C6-0997-08D9BFBA717A",
// 	"A124DE86-8A4F-4BE1-0998-08D9BFBA717A",
// 	"4BBC33F9-3305-43CC-7868-08D9BFBC63A5",
// 	"8930652F-77FF-4229-0BDF-08D9BFC25062"
// ]
// }