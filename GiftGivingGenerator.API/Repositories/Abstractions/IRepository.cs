using GiftGivingGenerator.API.Entities;

namespace GiftGivingGenerator.API.Repositories.Abstractions;

public interface IRepository<TEntity> where TEntity : IEntity
{
	TEntity Get(Guid id);
	TDto Get<TDto>(Guid id);
	Guid Insert(TEntity entity);
	void Update(TEntity entity);
}