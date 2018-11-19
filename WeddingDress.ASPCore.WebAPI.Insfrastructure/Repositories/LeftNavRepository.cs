using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces.ModelFactories;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.ViewModels;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories
{
    public class LeftNavRepository : ILeftNavRepository
    {
        private WeddingDressDataContext _context;
        private ILeftNavModelFactory _leftNavModelFactory;
        private IUtils _utils;

        public LeftNavRepository(WeddingDressDataContext context, ILeftNavModelFactory leftNavModelFactory, IUtils utils)
        {
            _context = context;
            _leftNavModelFactory = leftNavModelFactory;
            _utils = utils;
        }

        public void AddNewLeftNav(LeftNavViewModel leftNav)
        {
            var leftNavEntity = _leftNavModelFactory.CreateLeftNavEntity(leftNav);
            _context.LeftNavs.Add(leftNavEntity);
            _context.SaveChanges();
        }

        public void DeleteLeftNav(int id)
        {
            var leftNavs = _context.LeftNavs.Where(l => l.Id == id).Include(c => c.Childs);
            _context.RemoveRange(leftNavs);
            _context.SaveChanges();
        }

        public void EditLeftNav(LeftNavViewModel leftNav)
        {
            var leftNavEntity = _leftNavModelFactory.CreateLeftNavEntity(leftNav);
            _context.Entry(leftNavEntity).State = EntityState.Modified;
            if (leftNav.Childs != null && leftNav.Childs.Count > 0)
            {
                foreach (var child in leftNav.Childs)
                {
                    if (child.Id == 0)
                    {
                        _context.Entry(child).State = EntityState.Added;
                    }
                    else
                    {
                        _context.Entry(child).State = EntityState.Modified;
                    }
                }
            }
            _context.SaveChanges();
        }

        public async Task<LeftNav> GetLeftNav(int id)
        {
            return await _context.LeftNavs.Include(c => c.Childs).FirstOrDefaultAsync(c => c.Id == id);
        }

        public IEnumerable<LeftNav> GetLeftNavs()
        {
            return _context.LeftNavs.Include(a => a.Childs);
        }

        public int CountWithSearch(string searchStr)
        {
            if (!string.IsNullOrEmpty(searchStr))
            {
                return _context.LeftNavs.Count(p => p.Name.Contains(searchStr) || p.Url.Contains(searchStr));
            }
            return _context.LeftNavs.Count();
        }

        public IEnumerable<LeftNav> GetLeftNavsWithPagingAndSorting(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "Id";
            }

            IEnumerable<LeftNav> leftNavs = null;
            IOrderedQueryable<LeftNav> queryOrder = null;
            if (sort)
            {
                queryOrder = _utils.OrderBy(_context.LeftNavs, orderBy);
            }
            else
            {
                queryOrder = _utils.OrderByDescending(_context.LeftNavs, orderBy);
            }

            if (string.IsNullOrEmpty(search))
            {
                leftNavs = queryOrder
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).Include(c => c.Childs).ToList();
            }
            else
            {
                leftNavs = queryOrder
                    .Where(p => p.Name.Contains(search) || p.Url.Contains(search))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).Include(c=>c.Childs).ToList();
            }


            return leftNavs;
        }
    }
}
