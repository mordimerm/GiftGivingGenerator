// namespace GiftGivingGenerator.API;
//
// public class _NOTATKI
// {
// 	[ApiController]
// 	[Route("[controller]")]
// 	public class PersonController : ControllerBase
// 	{
// 		[HttpGet]
// 		public ActionResult<IEnumerable<Person>> GetAll()
// 		{
// 			var dbContext = new AppContext();
// 			var persons = dbContext.Persons.ToList();
//
// 			return Ok(persons);
// 		}
//
//
// 		[HttpPost]
// 		public IActionResult CreatePerson([FromBody] CreatePerson input)
// 		{
// 			Console.WriteLine(input.Name);
//
// 			var dbContext = new AppContext();
// 			var person = new Person
// 			{
// 				Name = input.Name,
// 			};
//
// 			dbContext.Add(person);
// 			dbContext.SaveChanges();
//
// 			return Ok();
// 		}
// 	}
//
// public class CreatePerson
// {
// 	public string Name { get; set; }
// }
//
// 	[HttpGet]
// 	public IActionResult Get([FromBody]MyModel myModel)
// 	{
// 		Console.WriteLine(myModel.Id);
// 		return Ok(myModel.Id);
// 	}
// }