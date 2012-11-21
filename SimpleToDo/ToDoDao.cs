using System;
using System.Collections.Generic;
using Castle.Transactions;
using NHibernate;

namespace SimpleToDo
{
	public class ToDoDao : IToDoDao
	{
		private readonly Func<ISession> getSession;

		public ToDoDao(Func<ISession> getSession)
		{
			this.getSession = getSession;
		}

		[Transaction]
		public IList<ToDoItem> List()
		{
			using (var session = getSession())
			{
				return session.CreateCriteria(typeof (ToDoItem)).List<ToDoItem>();
			}
		}

		[Transaction]
		public ToDoItem Get(long id)
		{
			using (var session = getSession())
			{
				return session.Get<ToDoItem>(id);
			}
		}

		[Transaction]
		public void Save(ToDoItem item)
		{
			using (var session = getSession())
			{
				session.SaveOrUpdate(item);
			}
		}

		[Transaction]
		public void Remove(ToDoItem item)
		{
			using (var session = getSession())
			{
				session.Delete(item);
			}
		}
	}
}
