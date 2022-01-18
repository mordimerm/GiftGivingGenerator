namespace GiftGivingGenerator.API;

public interface ISeeder
{
	void RemoveAllDataInDb();
	void Seed();
}