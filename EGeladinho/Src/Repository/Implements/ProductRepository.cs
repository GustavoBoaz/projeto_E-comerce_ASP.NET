using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EGeladinho.Src.Contexts;
using EGeladinho.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EGeladinho.Src.Repository.Implements
{
    public class ProductRepository : ICrud<Product> 
    {
        private readonly GeladinhoDBC _context;
    
        public ProductRepository(GeladinhoDBC context)
        {
            _context = context;
        }
    
        public async Task Create(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    
        /// <summary>
        ///     Read a product by id from the database.
        /// </summary>
        /// <param name="param"> Int param</param>
        public async Task<Product> Read(object param)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == (int) param);
        }
    
        public async Task Update(Product entity)
        {
            bool exist = await _context.Products.AnyAsync(p => p.Id == entity.Id);
    
            if (exist)
            {
                _context.Products.Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    
        public async Task Delete(object param)
        {
            bool exist = await _context.Products.AnyAsync(p => p.Id == (int) param);
    
            if (exist)
            {
                _context.Products.Remove(await _context.Products.FirstOrDefaultAsync(p => p.Id == (int) param));
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        
    }
}