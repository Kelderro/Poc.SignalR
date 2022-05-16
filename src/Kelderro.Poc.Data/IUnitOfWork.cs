using System;

namespace Kelderro.Poc.Data
{
	/// <summary>
	/// Encapsulates a unit of work
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

		/// <summary>
		/// Persists all updates to the data source and resets change tracking in the object context.
		/// </summary>
		/// <returns>The number of objects in an Added, Modified, or Deleted state when SaveChanges was called.</returns>
		bool SaveAll();
	}
}
