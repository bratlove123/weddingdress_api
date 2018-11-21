using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeddingDress.ASPCore.WebAPI.Insfrastructure.Models;

namespace WeddingDress.ASPCore.WebAPI.Insfrastructure.Interfaces
{
    public interface IUtils
    {
        IOrderedQueryable<TSource> OrderBy<TSource>(IQueryable<TSource> source, string propertyName);
        IOrderedQueryable<TSource> OrderByDescending<TSource>(IQueryable<TSource> source, string propertyName);
        byte[] GetByteArrayFromFile(IFormFile file);
    }
}
