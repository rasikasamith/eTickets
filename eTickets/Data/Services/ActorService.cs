using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>,IActorService
    {
        //private readonly AppDbContext _dbContext;

        public ActorService(AppDbContext appDbContext) : base(appDbContext) { } 
        

        //public async Task AddAsync(Actor actor)
        //{
        //    await _dbContext.Actors.AddAsync(actor);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var data =await _dbContext.Actors.FirstOrDefaultAsync(x => x.Id == id);
        //    _dbContext.Actors.Remove(data);
        //    await _dbContext.SaveChangesAsync();     
        //}

        //public async Task<IEnumerable<Actor>> GetAllAsync()
        //{
        //    var data = await _dbContext.Actors.ToListAsync();
        //    return data;
        //}

        //public async Task<Actor> GetByIdAsync(int id)
        //{
        //    var data = await _dbContext.Actors.FirstOrDefaultAsync(x => x.Id == id);
        //    return data;
        //}

        //public async Task<Actor> UpdateAsync(int id, Actor newActor)
        //{
        //    _dbContext.Update(newActor);
        //    await _dbContext.SaveChangesAsync();
        //    return newActor;
        //}      

        //Temp Added
        //public Actor GetById(int id)
        //{
        //    var data = _dbContext.Actors.FirstOrDefault(x => x.Id == id);
        //    return data;
        //}
        //public Actor Update(int id, Actor newActor)
        //{
        //    _dbContext.Update(newActor);
        //    _dbContext.SaveChanges();
        //    return newActor;
        //}

    }
}
