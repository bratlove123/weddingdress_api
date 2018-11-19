using System;
using System.Collections.Generic;
using System.Text;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models.Entities;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface IProjectRepository
    {
        int Count(string searchStr);
        IEnumerable<Project> GetProjectsWithPaging(int pageSize, int pageNumber, string orderBy, bool sort, string search);
        void Add(Project p);
        void Edit(Project p);
        void Remove(int id);
        void RemoveProjects(int[] ids);
        IEnumerable<Project> GetProjects();
        Project FindProjectById(int id);
        void Dispose();
    }
}
