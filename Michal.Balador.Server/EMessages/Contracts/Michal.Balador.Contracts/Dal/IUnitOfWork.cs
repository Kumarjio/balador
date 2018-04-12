using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Michal.Balador.Contracts.Dal
{
    //https://www.codeproject.com/Articles/316068/Restful-WCF-EF-POCO-UnitOfWork-Respository-MEF-o
    public interface IUnitOfWork: IDisposable
    {
        IQueryable<T> Get<T>() where T : class;

        Guid tid { get; set; }
        bool Remove<T>(T item) where T : class;


         Task Commit();

         void Attach<T>(T obj) where T : class;

        void Add<T>(T obj) where T : class;

        Database Database { get; }
    }
}
