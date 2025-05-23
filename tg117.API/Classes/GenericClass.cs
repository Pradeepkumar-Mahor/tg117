﻿using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.Json;
using System.Text;
using tg117.Domain;

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
                int count = await source.CountAsync();
                List<T> items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }

            public static PagedList<T> ReturnList(List<T> source, int pageNumber, int pageSize)
            {
                int count = source.Count;
                List<T> items = source.Skip(pageNumber * pageSize).Take(pageSize).ToList();
                return new PagedList<T>(items, count, pageNumber, pageSize);
            }

            internal static Task<PagedList<Category>> ReturnListAsync(List<Category> query, int pageNumber, int pageSize)
            {
                throw new NotImplementedException();
            }
        }

        public static string JSONSerialize<T>(T obj)
        {
            string retVal = String.Empty;
            using (MemoryStream ms = new())
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                DataContractJsonSerializer serializer = new(type: obj.GetType());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                serializer.WriteObject(ms, obj);
                byte[] byteArray = ms.ToArray();
                retVal = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            }
            return retVal;
        }
    }
}