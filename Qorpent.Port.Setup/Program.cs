using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Qorpent.Port.Setup
{
	class Program
	{
		static string g = "{D09BF67D-C2A7-466E-B982-FC6D8C99A6F7}";
		static void Main(string[] args)
		{
			if (!X509Found()){
				GenerateCertificates();
			}
			RegisterCertificates();
			var x509 = X509Certificate.CreateFromCertFile("qpt.cer");
			var th = x509.GetCertHashString();//.Replace("0","1");
			Console.WriteLine("Bind Port TO SS");
			Console.WriteLine("8092");
			var port = 8092;
			Process.Start(ResolveExeFile("netsh"), "http delete sslcert ipport=0.0.0.0:"+port).WaitForExit();
			Process.Start(ResolveExeFile("netsh"), "http add sslcert ipport=0.0.0.0:" + port + " certhash="+th+" appid="+g).WaitForExit();
		}

		private static void RegisterCertificates(){
			Console.WriteLine("Register CA");
			Process.Start(ResolveExeFile("certmgr"), "/c /add qpt.ca.cer  /r localMachine /s TrustedPublisher").WaitForExit();
			Console.WriteLine("Register Cert");
			Process.Start(ResolveExeFile("certmgr"), "/c /add qpt.cer  /r localMachine  /s AddressBook").WaitForExit();
		}

		static string ResolveExeFile(string name){
			if (!name.EndsWith(".exe")) name += ".exe";
			var paths = Environment.GetEnvironmentVariable("PATH").Split(';').ToList();
			paths.Add(@"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1A\Bin");
			paths.Add(Environment.GetFolderPath(Environment.SpecialFolder.System));
			
			return  paths.Select(_ => Path.Combine(_, name)).FirstOrDefault(File.Exists);
			
		}

		private static void GenerateCertificates(){
			Console.WriteLine("Generate CA");
			Process.Start(ResolveExeFile("makecert"), "-n \"CN=QorpentQlaudCA\" -r -sv qpt.ca.pvk qpt.ca.cer").WaitForExit();
			Console.WriteLine("Generate Cert");
			Process.Start(ResolveExeFile("makecert"), "-sk QorpentQlaud -iv qpt.ca.pvk -n \"CN=QorpentQlaud\" -ic qpt.ca.cer qpt.cer -sr localmachine -ss My").WaitForExit();
			

		}

		private static bool X509Found(){
			return new[]{"qpt.ca.pvk", "qpt.ca.cer", "qpt.cer"}.All(File.Exists);
		}
	}
}
