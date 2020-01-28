using System;
using System.Collections.Generic;

namespace MS.UnitOfWork.Collections
{
    /// <summary>
    /// 给<see cref="IEnumerable{T}"/>添加扩展方法来支持分页
    /// </summary>
    public static class IEnumerablePagedListExtensions
    {
        /// <summary>
        /// 在数据中取得固定页的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="indexFrom">起始页</param>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom = 1) => new PagedList<T>(source, pageIndex, pageSize, indexFrom);

        /// <summary>
        /// 在数据中取得固定页数据，并转换为指定数据类型
        /// </summary>
        /// <typeparam name="TSource">数据源类型</typeparam>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="converter"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="indexFrom">起始页</param>
        /// <returns></returns>
        public static IPagedList<TResult> ToPagedList<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int pageIndex, int pageSize, int indexFrom = 1) => new PagedList<TSource, TResult>(source, converter, pageIndex, pageSize, indexFrom);
    }
}
