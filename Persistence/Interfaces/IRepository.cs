using System;
using System.Collections.Generic;
using Model;

namespace Persistence.Interfaces;

public interface IRepository<T, TE> where TE : Entity<T>
{
	/// Returns the entity found by 'id' if it exists
	/// <param name="id">The id of the needed entity.</param>
	/// <returns>
    /// the entity with the given id if it exists
    /// null otherwise
    /// </returns>
    TE FindOne(T id);

	/// <returns>
    /// an IEnumerable with all the entities
    /// </returns>
    IEnumerable<TE> FindAll();

	/// Saves the given entity in the repository
	/// <param name="entity">The entity to save in the repository.</param>
	/// <returns>
    ///	null if the save succeeded
    /// the given entity otherwise
    /// </returns>
    bool Save(TE entity);

	/// Deletes the entity with the given ID
	/// <param name="id">The id of the wanted entity.</param>
	/// <returns>
    ///	null if the entity was not found
    /// the deleted entity otherwise
    /// </returns>
    Boolean Delete(T id);

	///  Updates the entity from the repository with the same ID as the given entity
	///  <param name="entity">The updated entity.</param>
	///  <returns>
	///  null if the update succeeded
	///	 the given entity otherwise
	///  </returns>
	Boolean Update(TE entity);
}