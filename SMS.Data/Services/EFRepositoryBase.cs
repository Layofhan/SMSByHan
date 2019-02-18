using SMS.Data.Interface;
using SMS.Entity;
using SMS.Entity.Result;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Data.Services
{
    public  class EFRepositoryBase<TEntity, TKey>: IRepository<TEntity,TKey> where TEntity : EntityBase<TKey>
    {
        #region 属性

        /// <summary>
        ///     获取 仓储上下文的实例
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }
        //public IUnitOfWork UnitOfWork = new UnitOfWork();
        //private readonly IUnitOfWork UnitOfWork;
        //public EFRepositoryBase(IUnitOfWork un)
        //{
        //    UnitOfWork = un;
        //}
        //public EFRepositoryBase()
        //{

        //}
        ///// <summary>
        /////     获取或设置 EntityFramework的数据仓储上下文
        ///// </summary>
        public IUnitOfWork EFContext
        {
            get
            {
                if (UnitOfWork is IUnitOfWork)
                {
                    return UnitOfWork as IUnitOfWork;
                }
                throw new Exception(string.Format("数据仓储上下文对象类型不正确，应为IUnitOfWorkContext，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }

        //public DbSet<TEntity> _dbSet;
        //public IUnitOfWork _unitOfWork;

        //public EFRepositoryBase(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //    //_dbSet = ((DbContext)unitOfWork).Set<TEntity>();
        //}
        //public EFRepositoryBase()
        //{
        //}
        /// <summary>
        /// 获取 当前单元操作对象
        /// </summary>
       // public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }
        ///// <summary>
        /////     获取 当前实体的查询数据集
        ///// </summary>
        //public virtual IQueryable<TEntity> Entities
        //{
        //    get { return UnitOfWork.Set<TEntity, TKey>(); }
        //}
        public virtual IQueryable<TEntity> Entities
        {
            get { return EFContext.Set<TEntity, TKey>(); }
        }
        #endregion

        #region 公共方法

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Insert(TEntity entity, bool isSave = true)
        {
            //DbContextBase context = new DbContextBase();
            //context.Set<TEntity>().Add(entity);
            //return isSave?context.SaveChanges():0;
            EFContext.RegisterNew<TEntity, TKey>(entity);
            //if (isSave)
            //{
            //    if (EFContext.Commit() >= 0)
            //        return DataProcess.Success();
            //    else
            //        return DataProcess.Failure();
            //}
            //return null;
            return isSave ? EFContext.Commit() : null;
            //_dbSet.Add(entity);
            //return isSave ? UnitOfWork.Commit() : 0;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            //EFContext.RegisterNew<TEntity, TKey>(entities);
            //return isSave ? EFContext.Commit() : 0;
            return 0;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Delete(object id, bool isSave = true)
        {
            TEntity entity = EFContext.Set<TEntity, TKey>().Find(id);
            return entity != null ? Delete(entity, isSave) : null;
            //return 0;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Delete(TEntity entity, bool isSave = true)
        {
            EFContext.RegisterDeleted<TEntity, TKey>(entity);
            return isSave ? EFContext.Commit() : null;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            //PublicHelper.CheckArgument(entities, "entities");
            //EFContext.RegisterDeleted<TEntity, TKey>(entities);
            //return isSave ? EFContext.Commit() : 0;
            return 0;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            //PublicHelper.CheckArgument(predicate, "predicate");
            //List<TEntity> entities = EFContext.Set<TEntity, TKey>().Where(predicate).ToList();
            //return entities.Count > 0 ? Delete(entities, isSave) : 0;
            return 0;
        }

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual DataResult Update(TEntity entity, bool isSave = true)
        {
            //PublicHelper.CheckArgument(entity, "entity");
            EFContext.RegisterModified<TEntity, TKey>(entity);
            return isSave ? EFContext.Commit() : null;
        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual TEntity GetByKey(object key)
        {
            //PublicHelper.CheckArgument(key, "key");
            return EFContext.Set<TEntity, TKey>().Find(key);

        }

        #endregion
    }
}

