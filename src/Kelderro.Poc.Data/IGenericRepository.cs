using System.Collections.Generic;
using System.Linq;

namespace Kelderro.Poc.Data
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		/// <summary>
		/// Gets all records as an IQueryable
		/// </summary>
		/// <returns>An IQueryable object containing the results of the query</returns>
		IQueryable<TEntity> Fetch();

		/// <summary>
		/// Gets all records as an IEnumberable
		/// </summary>
		/// <returns>An IEnumberable object containing the results of the query</returns>
		IEnumerable<TEntity> GetAll();

		/// <summary>
		/// Adds the given entity to the context underlying the set in the Added state such that it will be inserted into the database when SaveAll on the unitOfWork is called.
		/// </summary>
		/// <param name="entity">Entity to add</param>
		bool Add(TEntity entity);

		/// <summary>
		/// Update the given entity in the context underlying the set.
		/// </summary>
		/// <param name="originalEntity"></param>
		/// <param name="updatedEntity"></param>
		/// <returns></returns>
		bool Update(TEntity originalEntity, TEntity updatedEntity);

		/// <summary>
		/// Delete a record by primary key.
		/// </summary>
		/// <param name="keyValue"></param>
		/// <returns></returns>
		bool Delete(object keyValue);

		/// <summary>
		/// Delete a object from the database.
		/// </summary>
		/// <param name="entityToDelete"></param>
		/// <returns></returns>
		bool Delete(TEntity entityToDelete);
	}
}