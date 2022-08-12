using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Contexts;
using EGeladinho.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EGeladinho.Src.Repository.Implements
{
    public class UserRepository : ICrud<User>
    {
        private readonly GeladinhoDBC _context;

        public UserRepository(GeladinhoDBC context)
        {
            _context = context;
        }

        public async Task Create(User entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///     Read a user by id from the database.
        /// </summary>
        /// <param name="param"> Int param</param>
        public async Task<User> Read(object param)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == (int) param);
        }

        public async Task Update(User entity)
        {
            bool exist = await _context.Users.AnyAsync(u => u.Id == entity.Id);

            if (exist)
            {
                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task Delete(object param)
        {
            bool exist = await _context.Users.AnyAsync(u => u.Id == (int) param);

            if (exist)
            {
                _context.Users.Remove(await _context.Users.FirstOrDefaultAsync(u => u.Id == (int) param));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}