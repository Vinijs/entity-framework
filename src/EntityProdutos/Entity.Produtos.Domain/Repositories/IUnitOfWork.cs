using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Produtos.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}