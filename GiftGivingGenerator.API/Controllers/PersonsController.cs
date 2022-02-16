﻿using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GiftGivingGenerator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController : ControllerBase
{
	private readonly IPersonRepository _personRepository;

	public PersonsController(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}

	[HttpPost]
	public ActionResult Create([FromBody] CreatePersonDto dto)
	{
		var person = Person.Create(dto.Name, dto.Email);
		_personRepository.Insert(person);
		return Created($"/Persons/{person.Id}", person);
	}

	[HttpPut("{id}/Name")]
	public ActionResult ChangePersonName([FromRoute] Guid id, [FromBody] EditPersonDto get)
	{
		var person = _personRepository.Get(id);
		person.ChangeName(get.Name);

		_personRepository.Update(person);
		return Ok();
	}
}