using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.Facilities.NHibernate;
using Castle.Transactions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.Attributes;

namespace SimpleToDo
{
	public class NHibInstaller : INHibernateInstaller
	{
		private readonly Maybe<IInterceptor> interceptor;
		private Configuration config;

		public NHibInstaller()
		{
			interceptor = Maybe.None<IInterceptor>();
		}

		public void Registered(ISessionFactory factory)
		{
		}

		public bool IsDefault
		{
			get { return true; }
		}

		public string SessionFactoryKey
		{
			get { return "default"; }
		}

		public Maybe<IInterceptor> Interceptor
		{
			get { return interceptor; }
		}

		public Configuration Config
		{
			get 
			{
				if (config == null)
				{
					IDictionary<string, string> props = new Dictionary<string, string>()
					                                    	{
																{"connection.driver_class", "NHibernate.Driver.NpgsqlDriver"},
																{"connection.connection_string", "Server=10.211.55.2; Port=5432; User Id=test; Password=test; Database=testdb;"},
					                                    		{"dialect", "NHibernate.Dialect.PostgreSQL82Dialect"}, 
																{"show_sql", "true"}
					                                    	};
					HbmSerializer.Default.Serialize(Assembly.GetExecutingAssembly(), "schema.hbm.xml");
					config = new Configuration()
						.SetProperties(props)
						.AddInputStream(HbmSerializer.Default.Serialize(Assembly.GetExecutingAssembly()));
				}
				return config;
			}
		}
	}
}
