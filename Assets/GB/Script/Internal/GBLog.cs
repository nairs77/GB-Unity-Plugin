#if GB_DEBUG
#define LOG_FLAG_ERROR
#define LOG_FLAG_WARN
#define LOG_FLAG_INFO
#define LOG_FLAG_VERBOSE
#elif GB_RELEASE
#define LOG_FLAG_ERROR
#define LOG_FLAG_WARN
#else
#define LOG_FLAG_ERROR
#define LOG_FLAG_WARN
#endif

using UnityEngine;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

public static class GBLog {
	
	[System.Diagnostics.Conditional("LOG_FLAG_ERROR")]
	public static void error(object message, params object[] paramList) {
		UnityEngine.Debug.Log(GetMessage(message, paramList));		
	}
		
//	[System.Diagnostics.Conditional("LOG_FLAG_ERROR")]
	[System.Diagnostics.Conditional("LOG_FLAG_WARN")]
	public static void warn(object message, params object[] paramList) {
		UnityEngine.Debug.Log(GetMessage(message, paramList));		
	}
	
	//  [System.Diagnostics.Conditional("LOG_FLAG_ERROR")]
	//  [System.Diagnostics.Conditional("LOG_FLAG_WARN")]
	[System.Diagnostics.Conditional("LOG_FLAG_INFO")]
	public static void info(object message, params object[] paramList) {
		UnityEngine.Debug.Log(GetMessage(message, paramList));		
	}		
	
	//  [System.Diagnostics.Conditional("LOG_FLAG_ERROR")]
	//  [System.Diagnostics.Conditional("LOG_FLAG_WARN")]
	//  [System.Diagnostics.Conditional("LOG_FLAG_INFO")]
	[System.Diagnostics.Conditional("LOG_FLAG_VERBOSE")]
	public static void verbose(object message, params object[] paramList) {
		UnityEngine.Debug.Log(GetMessage(message, paramList));
	}
	
	[System.Diagnostics.Conditional("LOG_FLAG_VERBOASE")]
	public static void assert(bool condition, string message) {
		assert(condition, message);
	}
	
	static string GetMessage(object errorMsg, params object[] paramList) {
		StringBuilder sb = new StringBuilder();
		sb.Append("[GBLog] ");
		sb.Append(CallStackInfo());
				
		if (errorMsg is string) {
			sb.Append((string)errorMsg);		
		} else {			
			for (int i = 0; i < paramList.Length; i++) {
				sb.Append(paramList[i].ToString());
				sb.Append("\t");
			}
		}
		string s = sb.ToString();
			
		return s;		
	}
	
	static string CallStackInfo() {
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