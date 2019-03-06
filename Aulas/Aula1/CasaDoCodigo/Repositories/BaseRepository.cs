using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class BaseRepository<T> where T: BaseModel
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
    }
}
