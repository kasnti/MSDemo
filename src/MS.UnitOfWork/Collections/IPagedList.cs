using System.Collections.Generic;

namespace MS.UnitOfWork.Collections
{
    /// <summary>
    /// 提供任何类型的分页接口
    /// </summary>
    /// <typeparam name="T">需要分页的数据类型</typeparam>
    public interface IPagedList<T>
    {
        /// <summary>
        /// 起始页 值
        /// </summary>
        int IndexFrom { get; }
        /// <summary>
        /// 当前页 值
        /// </summary>
        int PageIndex { get; }
        /// <summary>
        /// 每页大小
        /// </summary>
        int PageSize { get; }
        /// <summary>
        /// 数据总数
        /// </summary>
        int TotalCount { get; }
        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }
        /// <summary>
        /// 当前页数据
        /// </summary>
        IList<T> Items { get; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        bool HasPreviousPage { get; }
        /// <summary>
        /// 是否有下一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
