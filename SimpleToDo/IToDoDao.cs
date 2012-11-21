using System.Collections.Generic;

namespace SimpleToDo
{
	/// <summary>
	/// Interface for DAO
	/// </summary>
	public interface IToDoDao
	{
		IList<ToDoItem> List();
		ToDoItem Get(long id);
		void Save(ToDoItem item);
		void Remove(ToDoItem item);
	}
}
