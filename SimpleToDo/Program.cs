using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Nancy.Hosting.Self;

namespace SimpleToDo
{
	class Program
	{
		private static readonly Logger logger = LogManager.GetCurrentClassLogger();

		public static int Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Usage: SimpleTodo.exe <url>");
				return -1;
			}

			Uri uri;
			try
			{
				uri = new Uri(args[0]);
			}
			catch (UriFormatException ex)
			{
				Console.WriteLine("Please check URI format in {0}", args[0]);
				return -1;
			}

			CancellationTokenSource tokenSource = new CancellationTokenSource();
			CancellationToken ct = tokenSource.Token;

			var nancyHost = new NancyHost(uri);

			Task nancyTask = new Task(() =>
			{
				nancyHost.Start();

				Console.WriteLine("Nancy now listening");

				while (!ct.IsCancellationRequested)
				{
					Thread.Sleep(1000);
				}
				nancyHost.Stop();  // stop hosting
			}, tokenSource.Token);

			nancyTask.Start();
			Console.WriteLine("Server started. Enter 'quit' to stop.");

			var line = Console.ReadLine();
			while (line != "quit")
			{
				line = Console.ReadLine();
			}
			tokenSource.Cancel();

			return 0;
		}
	}
}
