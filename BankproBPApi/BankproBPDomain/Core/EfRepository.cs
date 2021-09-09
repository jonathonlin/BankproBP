using BankproBPData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Core
{
	public class EfRepository<T> : IRepository<T> where T : class
	{
		protected BankproBpDbContext _db;

		public EfRepository(BankproBpDbContext db)
		{
			_db = db;
		}
		public T Add(T t)
		{
			_db.Set<T>().Add(t);
			_db.SaveChanges();
			return t;
		}

		public async Task<T> AddAsyn(T t)
		{
			_db.Set<T>().Add(t);
			await _db.SaveChangesAsync();
			return t;
		}

		public int Count()
		{
			return _db.Set<T>().Count();
		}

		public async Task<int> CountAsync()
		{
			return await _db.Set<T>().CountAsync();
		}

		public void Delete(T entity)
		{
			_db.Set<T>().Remove(entity);
			_db.SaveChanges();
		}

		public async Task<int> DeleteAsyn(T entity)
		{
			_db.Set<T>().Remove(entity);
			return await _db.SaveChangesAsync();
		}

		private bool disposed = false;
		protected void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_db.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public T Find(Expression<Func<T, bool>> match)
		{
			return _db.Set<T>().SingleOrDefault(match);
		}

		public ICollection<T> FindAll(Expression<Func<T, bool>> match)
		{
			return _db.Set<T>().Where(match).ToList();
		}

		public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
		{
			return await _db.Set<T>().Where(match).ToListAsync();
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> match)
		{
			return await _db.Set<T>().SingleOrDefaultAsync(match);
		}

		public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
		{
			return _db.Set<T>().Where(predicate);
		}

		public async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
		{
			return await _db.Set<T>().Where(predicate).ToListAsync();
		}

		public T Get(int id)
		{
			return _db.Set<T>().Find(id);
		}

		public IQueryable<T> GetAll()
		{
			return _db.Set<T>();
		}

		public async Task<ICollection<T>> GetAllAsyn()
		{
			return await _db.Set<T>().ToListAsync();
		}

		public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
		{
			IQueryable<T> queryable = GetAll();
			foreach (Expression<Func<T, object>> includeProperty in includeProperties)
			{
				queryable = queryable.Include<T, object>(includeProperty);
			}
			return queryable;
		}


		public async Task<T> GetAsync(int id)
		{
			return await _db.Set<T>().FindAsync(id);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public async Task<int> SaveAsync()
		{
			return await _db.SaveChangesAsync();
		}

		public T Update(T t, object key)
		{
			if (t == null)
				return null;
			T exist = _db.Set<T>().Find(key);
			if (exist != null)
			{
				_db.Entry(exist).CurrentValues.SetValues(t);
				_db.SaveChanges();
			}
			return exist;
		}

		public async Task<T> UpdateAsyn(T t, object key)
		{
			if (t == null)
				return null;
			T exist = await _db.Set<T>().FindAsync(key);
			if (exist != null)
			{
				_db.Entry(exist).CurrentValues.SetValues(t);
				await _db.SaveChangesAsync();
			}
			return exist;
		}
	}
}
