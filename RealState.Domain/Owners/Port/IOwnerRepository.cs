using RealState.Domain.Owners.Entity;

namespace RealState.Domain.Owners.Port
{
    /// <summary>
    /// Defines the contract for a repository that handles operations related to owners.
    /// </summary>
    public interface IOwnerRepository
    { 
        /// <summary>
        /// Adds a new owner to the repository.
        /// </summary>
        /// <param name="owner">The owner to add.</param>
        /// <returns>A task that will eventually resolve to an integer representing the ID of the newly added owner.</returns>
        Task<Guid> AddAsync(Owner owner);
    }
}
