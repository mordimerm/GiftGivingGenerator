using AutoMapper;
using AutoMapper.QueryableExtensions;
using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
	protected readonly AppContext DbContext;
	protected readonly IMapper Mapper;

	protected RepositoryBase(AppContext dbContext, IMapper mapper)
	{
		DbContext = dbContext;
		Mapper = mapper;
	}

	public virtual IQueryable<TEntity> WriteEntitySet()
	{
		return DbContext.Set<TEntity>();
	}

	public TEntity Get(Guid id)
	{
		return WriteEntitySet().Single(x => x.Id == id);
	}
	
	public TDto Get<TDto>(Guid id)
	{
		return DbContext.Set<TEntity>()
			.Where(x => x.Id == id)
			.ProjectTo<TDto>(Mapper.ConfigurationProvider)
			.Single();
	}
	
	public Guid Insert(TEntity entity)
	{
		DbContext.Set<TEntity>().Add(entity);
		DbContext.SaveChanges();

		return entity.Id;
	}

	public void Update(TEntity entity)
	{
		DbContext.Set<TEntity>().Update(entity);
		DbContext.SaveChanges();
	}
}