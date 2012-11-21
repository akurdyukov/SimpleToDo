using Castle.Facilities.AutoTx;
using Castle.Facilities.NHibernate;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Nancy.Bootstrappers.Windsor;
using Nancy.Conventions;
using Nancy.Diagnostics;

namespace SimpleToDo
{
	public class LocalBootstrapper : WindsorNancyBootstrapper
	{
		protected override DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get
			{
				return new DiagnosticsConfiguration { Password = @"123" };
			}
		}

		protected override void ConfigureApplicationContainer(IWindsorContainer container)
		{
			base.ConfigureApplicationContainer(container);

			// TODO: install facilities here
		    container
		        .AddFacility<AutoTxFacility>()
		        .Register(
		            Component.For<INHibernateInstaller>().ImplementedBy<NHibInstaller>().LifestyleSingleton(),
		            Component.For<IToDoDao>().ImplementedBy<ToDoDao>().LifestyleSingleton()
		        )
		        .AddFacility<NHibernateFacility>();
		}

		protected override void ConfigureConventions(NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);

			nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("assets", @"Assets"));
		}
	}
}
