using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    // generic constraint
    // class : referans tip olabilir
    // new() : new'lenebilir olmalı
    public interface IEntityRepository<T> where T:class, IEntity,new()
    {
        // delege,filtreleme yapısı. Expression = filtreleme mantığı - E-Ticaret sitelerinde filtreleme işlemi gibi düşün
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
