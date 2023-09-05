using Bulky.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository categoryRepository { get; private set; }
        public IProductRepository productRepository { get; private set; }
        public ICompanyRepository companyRepository { get; private set; }
        public IShoppingCartRepository shoppingCartRepository { get; private set; } 
        public IApplicationUserRepository applicationUserRepository { get; private set; }
        public IOrderDetailRepository orderDetailRepository { get; private set; }
        public IOrderHeaderRepository orderHeaderRepository { get; private set; }

        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            categoryRepository = new CategoryRepository(_db);
            productRepository = new ProductRepository(_db);
            companyRepository = new CompanyRepository(_db);
            shoppingCartRepository = new ShoppingCartRepository(_db);
            applicationUserRepository = new ApplicationUserRepository(_db);
            orderDetailRepository = new OrderDetailRepository(_db);
            orderHeaderRepository  = new OrderHeaderRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
