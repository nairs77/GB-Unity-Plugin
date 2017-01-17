using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

// Tail with this to see output clearly:
//
//   tail -f ~/Library/Logs/Unity/Editor.log  | grep ^GB --line-buffered | awk '{ printf("%-9s %-60s", $1, $2); $1=$2=""; printf("%s\n", $0); }'

namespace GB {
	public class GBLogger {

		public static void Info(string msg) {
			System.Console.WriteLine("[GBLogger] : Info {0}", msg);
		}

		public static void Warn(string msg) {
			System.Console.WriteLine("[GBLogger] : Warning {0}", msg);
		}

		public static void Error(string msg) {
			System.Console.WriteLine("[GBLogger] : Error {0} {1}", StackInfo(), msg);
		}

		protected static string StackInfo() {
			StackTrace t = new System.Diagnostics.StackTrace(true);
			StackFrame sf = t.GetFrame(2);
			if (sf != null) {
				string filename = sf.GetFileName();
				if (filename != null) {
					filename = Regex.Replace(filename, @".*?Assets\/(.*)", "$1");
					int line = sf.GetFileLineNumber();
					return filename + ":" + line;
				}
			}
			return "";
		}
	}
}