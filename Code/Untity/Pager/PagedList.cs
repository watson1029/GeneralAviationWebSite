using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untity
{
    public interface IPagedList
    {
        int TotalCount { get; set; }
        int TotalPages { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
        bool IsPreviousPage { get; }
        bool IsNextPage { get; }
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList() { }
        public PagedList(IQueryable<T> source, int index, int pageSize)
        {
            int total = source.Count();
            TotalCount = total;
            TotalPages = total/pageSize;

            if (total%pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = index;
            AddRange(source.Skip((index - 1) * pageSize).Take(pageSize).ToList());
        }

        public PagedList(IEnumerable<T> source, int totalRecords, int index, int pageSize)
        {
            TotalCount = totalRecords;
            TotalPages = totalRecords/pageSize;

            if (totalRecords%pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = index;
            AddRange(source.ToList());
        } 
        #region IPagedList Members
        [JsonIgnore]
        public int TotalPages { get; set; }
        //[JsonIgnore]
        public int TotalCount { get; set; }
        [JsonIgnore]
        public int PageIndex { get; set; }
        [JsonIgnore]
        public int PageSize { get; set; }
        [JsonIgnore]
        public bool IsPreviousPage
        {
            get { return ((PageIndex - 1) > 0); }
        }
        [JsonIgnore]
        public bool IsNextPage
        {
            get { return (PageIndex * PageSize) <= TotalCount; }
        }

        #endregion
    }

    public static class Pagination
    {
        #region IQueryable<T> extensions

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index)
        {
            return new PagedList<T>(source, index, 10);
        }

        #endregion

        #region IEnumerable<T> extensions

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source.AsQueryable(), pageIndex, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            return new PagedList<T>(source, totalCount, pageIndex, pageSize);
        }
         
        #endregion
    }
}
