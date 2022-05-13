using eTickets.Data.Base;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IActorService: IEntityBaseRepository<Actor>
    {
        //Task<IEnumerable<Actor>> GetAllAsync();

        //Task<Actor> GetByIdAsync(int id);

        //Task AddAsync(Actor actor);

        //Task<Actor> UpdateAsync(int id, Actor newActor);       

        //Task DeleteAsync(int id);

        //Temp Added
        //Actor Update(int id, Actor newActor);
        //Actor GetById(int id);
    }
}
