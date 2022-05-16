using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Kelderro.Poc.Data
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		private readonly PocContext _context;
		private readonly IDbSet<TEntity> _dbSet;

		public GenericRepository(PocContext context)
		{
			_context = context;
			_dbSet = context.Set<TEntity>();
		}

		public IQueryable<TEntity> Fetch()
		{
			return _dbSet;
		}

		public IEnumerable<TEntity> GetAll()
		{
			return Fetch().AsEnumerable();
		}

		public virtual bool Add(TEntity entity)
		{
			try
			{
				_dbSet.Add(entity);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public virtual bool Update(TEntity originalEntity, TEntity updatedEntity)
		{
			try
			{
				_context.Entry(originalEntity).CurrentValues.SetValues(updatedEntity);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
			
		}

		public virtual bool Delete(object keyValue)
		{
			try
			{
				var entityToDelete = _dbSet.Find(keyValue);
				if (entityToDelete != null)
				{
					Delete(entityToDelete);
					return true;
				}
			}
			catch (Exception)
			{
				// TODO logging
			}

			return false;
		}

		public virtual bool Delete(TEntity entityToDelete)
		{
			try
			{
				if (_context.Entry(entityToDelete).State == EntityState.Detached)
					_dbSet.Attach(entityToDelete);

				_dbSet.Remove(entityToDelete);
				return true;
			}
			catch (Exception)
			{
				// TODO logging
			}

			return false;
		}
	}
}