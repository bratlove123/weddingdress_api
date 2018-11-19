using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Ef;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly WeddingDressDataContext _context;
        private readonly IUtils _utils;

        public ProjectRepository(WeddingDressDataContext context, IUtils utils)
        {
            _context = context;
            _utils = utils;
        }

        public void Add(Project p)
        {
            _context.Projects.Add(p);
            _context.SaveChanges();
        }

        public int Count(string searchStr)
        {
            if (!string.IsNullOrEmpty(searchStr))
            {
                return _context.Projects.Count(p => p.Name.Contains(searchStr));
            }
            return _context.Projects.Count();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Edit(Project p)
        {
            p.ModifiedDate = DateTime.Now;
            _context.Entry(p).State = EntityState.Modified;
        }

        public Project FindProjectById(int id)
        {
            Project p = _context.Projects.FirstOrDefault(c => c.Id == id);
            return p;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects;
        }

        public IEnumerable<Project> GetProjectsWithPaging(int pageSize, int pageNumber, string orderBy, bool sort, string search)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "Id";
            }

            IEnumerable<Project> projects = null;
            IOrderedQueryable<Project> queryOrder = null;
            if (sort)
            {
                queryOrder = _utils.OrderBy(_context.Projects, orderBy);
            }
            else
            {
                queryOrder = _utils.OrderByDescending(_context.Projects, orderBy);
            }

            if (string.IsNullOrEmpty(search))
            {
                projects = queryOrder
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }
            else
            {
                projects = queryOrder
                    .Where(p => p.Name.Contains(search))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();
            }


            return projects;
        }

        public void Remove(int id)
        {
            Project p = FindProjectById(id);
            _context.Projects.Remove(p);
            _context.SaveChanges();
        }

        public void RemoveProjects(int[] ids)
        {
            foreach (var id in ids)
            {
                Project p = FindProjectById(id);
                _context.Projects.Remove(p);
                _context.SaveChanges();
            }
        }
    }
}
