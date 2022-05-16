using System;
using System.Collections.Generic;
using System.Linq;

namespace Kelderro.Poc.Data
{
	public class UnitOfWork<TContext> : IUnitOfWork where TContext : PocContext, new()
	{
		private readonly PocContext _ctx;
		private readonly Dictionary<Type, object> _repositories;
		private bool _disposed;

		public UnitOfWork()
		{
			_ctx = new TContext();
			_repositories = new Dictionary<Type, object>();
			_disposed = false;
		}

		public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			if (_repositories.Keys.Contains(typeof(TEntity)))
				return _repositories[typeof(TEntity)] as IGenericRepository<TEntity>;

			var repository = new GenericRepository<TEntity>(_ctx);
			_repositories.Add(typeof(TEntity), repository);
			return repository;
		}

		public bool SaveAll()
		{
			return _ctx.SaveChanges() > 0;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;

			if (disposing)
				_ctx.Dispose();

			_disposed = true;
		}
	}
}