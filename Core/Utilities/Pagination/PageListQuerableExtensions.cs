using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Pagination
{
    public static class PageListQuerableExtensions
    {
        public static async Task<PaginationResult<T>> ToPagedListAsync<T>(
            this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count= await source.CountAsync();
            if (count>0)
            {
                var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .ToListAsync();
                return new(items,pageNumber,pageSize,count);
            }
            return new(null, 0, 0, 0);
        }

        public static PaginationResult<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            if (count>0)
            {
                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .ToList();
                return new(items, pageNumber, pageSize, count);
            }
            return new(null, 0, 0, 0);
        }
    }
}
