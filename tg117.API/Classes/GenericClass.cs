using Microsoft.EntityFrameworkCore;
using static tg117.API.Classes.GenericClass;

namespace tg117.API.Classes
{
    public static class GenericClass
    {
        public class PaginatedModel
        {
            public int count { get; set; }
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
        }

        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPages { get; private set; }

            public PaginatedList(List<T> items, PaginatedModel paginatedModel)
            {
                PageIndex = paginatedModel.pageIndex;
                TotalPages = (int)Math.Ceiling(paginatedModel.count / (double)paginatedModel.pageSize);

                this.AddRange(items);
            }

            public bool HasPreviousPage => PageIndex > 1;

            public bool HasNextPage => PageIndex < TotalPages;

            public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = await source.CountAsync();
                var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                PaginatedModel paginatedModel = new PaginatedModel();
                paginatedModel.count = count;
                paginatedModel.pageIndex = pageIndex;
                paginatedModel.pageSize = pageSize;
                return new PaginatedList<T>(items, paginatedModel);
            }
        }
    }
}