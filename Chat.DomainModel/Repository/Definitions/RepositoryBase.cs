using Chat.DomainModel.Repository.Interfaces;
using System;
using System.Collections.Generic;
using Chat.DomainModel.Context;
using Chat.DomainModel.Domain;
using System.Data.Entity.Core;
using System.Data.Entity;

namespace Chat.DomainModel.Repository.Definitions
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseModel
	{
		public ChatDbContext _context { get; }
		private bool disposed = false;

		public RepositoryBase(ChatDbContext context)
		{
			_context = context;
		}

		#region [Read Methods]

		public IEnumerable<T> GetAll() =>
			_context.Set<T>();

		public T GetById(Guid id) =>
			_context.Set<T>().Find(id);

		#endregion

		#region [Write Methods]

		public T Add(T item)
		{
			item.Id = item.Id == Guid.Empty ? Guid.NewGuid() : item.Id;
			var entity = _context.Set<T>().Add(item);
			_context.SaveChanges();

			return entity;
		}

		public bool Update(T item)
		{
			try
			{
				_context.Entry(item).State = EntityState.Modified;
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public void Delete(T item)
		{
			try
			{
				_context.Set<T>().Remove(item);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new UpdateException("Brisanje neuspešno, proverite veze između tabela.", ex);
			}
		}

		#endregion

		#region [Dispose]

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}