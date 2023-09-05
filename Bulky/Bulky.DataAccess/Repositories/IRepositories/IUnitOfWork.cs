using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository categoryRepository { get; }
        IProductRepository productRepository { get; }
        ICompanyRepository companyRepository { get; }
        IShoppingCartRepository shoppingCartRepository { get; }
        IApplicationUserRepository applicationUserRepository { get; }
        IOrderDetailRepository orderDetailRepository { get; }
        IOrderHeaderRepository orderHeaderRepository { get; }

        void Save();
    }
}
