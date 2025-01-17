using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Serialization.Json;
using System.Text;
using static tg117.API.Classes.GenericClass;

namespace tg117.API.Classes
{
    public static class GenericClass
    {
        public class QueryParams
        {
            private const int MaxPageSize = 50;
            public int PageNumber { get; set; } = 1;
            private int _pageSize = 10;

            public int PageSize
            {
                get => _pageSize;
                set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public class PagedList<T> : List<T>
        {
            public PagedList(IEnumerable<T> currentPage, int count, int pageNumber, int pageSize)
            {
                CurrentPage = pageNumber;
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                PageSize = pageSize;
                TotalCount = count;
                AddRange(currentPage);
            }

            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }

            public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
            {
                var count = await source.CountAsync();
                var items = await source.Skip((pageNumber) * pageSize).Take(pageSize).ToListAsync();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }

            public static async Task<PagedList<T>> ReturnListAsync(List<T> source, int pageNumber, int pageSize)
            {
                var count = source.Count();
                var items = source.Skip((pageNumber) * pageSize).Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }
        }

        public static string JSONSerialize<T>(T obj)
        {
            string retVal = String.Empty;
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(ms, obj);
                var byteArray = ms.ToArray();
                retVal = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
            return retVal;
        }
    }
}