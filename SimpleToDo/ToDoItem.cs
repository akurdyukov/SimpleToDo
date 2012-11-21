using System;
using NHibernate.Mapping.Attributes;

namespace SimpleToDo
{
	[Serializable]
	[Class(Table = "TODOITEMS")]
	public class ToDoItem
	{
		[Id(0, Column = "ID", Name = "Id")]
		[Generator(1, Class = "native")]
		public virtual long Id { get; protected set; }

		[Property(Column = "NAME", NotNull = true)]
		public virtual string Name { get; set; }

		[Property(Column = "CREATED_AT")]
		public virtual DateTime CreatedAt { get; set; }
	}
}
