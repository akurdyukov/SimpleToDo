using System;
using System.Dynamic;
using System.Linq;
using DotLiquid;
using Nancy;
using Nancy.ModelBinding;

namespace SimpleToDo
{
	public class MainModule : NancyModule
	{
		private readonly IToDoDao dao;

		public MainModule(IToDoDao dao)
		{
			this.dao = dao;
			Get["/"] = RenderMain;
			Post["/"] = parameters =>
			            	{
			            		var itemModel = this.Bind<ToDoItemModel>();
								
								ToDoItem item = new ToDoItem {CreatedAt = DateTime.Now, Name = itemModel.Name};
								dao.Save(item);

			            		return RenderMain(null);
			            	};
			Delete["/{id}"] = parameters =>
			                  	{
			                  		long id;
									if (long.TryParse(parameters.id, out id))
									{
										var item = dao.Get(id);
										if (item != null)
										{
											dao.Remove(item);
										}
									}
									return RenderMain(null);
								};
		}

		private dynamic RenderMain(dynamic parameters)
		{
			dynamic model = new ExpandoObject();
            model.Items = dao.List().Select(Hash.FromAnonymousObject).ToList();
			return View["index", model];
		}
	}
}
