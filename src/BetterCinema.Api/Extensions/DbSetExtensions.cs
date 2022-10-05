using BetterCinema.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BetterCinema.Api.Extensions
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> GetSetSection<T>(this DbSet<T> set, int limit, int offset) where T : class
        {
            limit = Math.Max(limit, 0);
            offset = Math.Max(offset, 0);

            IQueryable<T> setSection = set.Skip(offset);
        
            if (limit > 0)
            {
                setSection = setSection.Take(limit);
            }
            
            return setSection;
        }
    }
}