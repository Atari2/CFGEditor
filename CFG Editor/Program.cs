using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFG
{
	static class Loader {
		public static void DynamicLoad(Assembly assembly, string dllname) {
			string resourceName = "CFG.Resources." + dllname;
			using (Stream s = assembly.GetManifestResourceStream(resourceName)) {
				byte[] data = new BinaryReader(s).ReadBytes((int)s.Length);
				string assemblyPath = Path.GetDirectoryName(assembly.Location);
				if (assemblyPath is null) {
					assemblyPath = Directory.GetCurrentDirectory();
                }
				string templDllPath = Path.Combine(assemblyPath, dllname);
				File.WriteAllBytes(templDllPath, data);
            }
        }
    }
	static class Program
	{
		/// <summary>
		/// The main entry point of the progam.
        /// Other than passing the calling arguments into the CFG Editor, nothing new happens here.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (!File.Exists("Newtonsoft.Json.dll")) {
				Loader.DynamicLoad(Assembly.GetExecutingAssembly(), "Newtonsoft.Json.dll");
            }
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new CFG_Editor(args));
		}
	}
}
