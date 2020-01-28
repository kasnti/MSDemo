using MS.UnitOfWork.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MS.UnitOfWork
{
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region GetAll
        /// <summary>
        ///获取所有实体
        ///注意性能！
        /// </summary>
        /// <returns>The <see cref="IQueryable{TEntity}"/>.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 获取所有实体，必须提供筛选谓词
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">筛选谓词</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <returns></returns>
        IQueryable<TResult> GetAll<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false
            ) where TResult : class;

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);
        #endregion

        #region GetPagedList
        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        IPagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<IPagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        IPagedList<TResult> GetPagedList<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false
            ) where TResult : class;


        /// <summary>
        /// 获取分页数据
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="pageIndex">当前页。默认第一页</param>
        /// <param name="pageSize">页大小。默认20笔数据</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int pageIndex = 1,
            int pageSize = 20,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default) where TResult : class;

        #endregion

        #region GetFirstOrDefault
        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        TEntity GetFirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <returns></returns>
        TResult GetFirstOrDefault<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);

        /// <summary>
        /// 获取满足条件的序列中的第一个元素
        /// 如果没有元素满足条件，则返回默认值
        /// 默认是关闭追踪查询的（拿到的数据默认只读）
        /// 默认开启全局查询筛选过滤
        /// </summary>
        /// <typeparam name="TResult">输出数据类型</typeparam>
        /// <param name="selector">投影选择器</param>
        /// <param name="predicate">条件表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="include">包含的导航属性</param>
        /// <param name="disableTracking">设置为true关闭追踪查询。默认为true</param>
        /// <param name="ignoreQueryFilters">设置为true忽略全局查询筛选过滤。默认为false</param>
        /// <param name="cancellationToken">异步token</param>
        /// <returns></returns>
        Task<TResult> GetFirstOrDefaultAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false,
            CancellationToken cancellationToken = default);


        #endregion

        #region Find
        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
        ValueTask<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        #endregion

        #region sql、count、exist
        /// <summary>
        /// 使用原生sql查询来获取指定数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 按指定条件元素是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Insert
        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(params TEntity[] entities);


        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        void Insert(IEnumerable<TEntity> entities);


        /// <summary>
        /// Inserts a new entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities asynchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        #endregion

        #region Update
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(params TEntity[] entities);

        /// <summary>
        /// Updates the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Update(IEnumerable<TEntity> entities);
        #endregion

        #region Delete
        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void Delete(IEnumerable<TEntity> entities);
        #endregion
    }
}
