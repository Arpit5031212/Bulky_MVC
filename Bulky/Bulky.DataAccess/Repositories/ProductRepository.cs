using Bulky.DataAccess.Repositories.IRepositories;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (objFromDb != null)
            {
                obj.Title = objFromDb.Title;
                obj.ISBN = objFromDb.ISBN;
                obj.Price = objFromDb.Price;
                obj.Price50 = objFromDb.Price50;
                obj.ListPrice = objFromDb.ListPrice;
                obj.Price100 = objFromDb.Price100;
                obj.Description = objFromDb.Description;
                obj.CategoryId = objFromDb.CategoryId;
                obj.Author = objFromDb.Author;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
