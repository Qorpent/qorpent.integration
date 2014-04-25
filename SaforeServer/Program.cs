using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qorpent.Host;

namespace SaforeServer
{
	class Program
	{
		static void Main(string[] args)
		{
			var server = new HostServer(8093);
			server.Start();
			Console.ReadLine();
		}
	}
}
