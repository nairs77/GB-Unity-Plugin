#if UNITY_IPHONE
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

public class GBPostprocessScript : MonoBehaviour 
{
	/**
	 *  only Unity 4.3.4 or earlier
	 
	//const string _patchHeader = "#include <OpenGLES/ES2/glext.h>";
	//const string _oldHeader = "#include <OpenGLES/ES2/gl.h>";
	 */
	const string _oldAppMain = "UIApplicationMain(argc, argv, nil, [NSString stringWithUTF8String:AppControllerClassName]);";
	const string _patchAppMain = "UIApplicationMain(argc, argv, [NSString stringWithUTF8String:\"JPLApplication\"], [NSString stringWithUTF8String:AppControllerClassName]);";

	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject) {
		UnityEngine.Debug.Log("-- Custom PostprocessBuild -- Excute post process build phrase.");

	#if UNITY_5
		if (target != BuildTarget.iOS){
	#else
		if (target != BuildTarget.iPhone){
	#endif
			return;	
		}
			
		
		/* Only Unity 4.3.4 or earlier
		GBPostprocessScript.ReplaceOpenGL(pathToBuildProject);
		*/
		//GBPostprocessScript.ReplaceApplicationMain(pathToBuildProject);

		/* Change Your FrameworkPath */
		UnityEngine.Debug.Log("--- !!!!!!!!!!! ---");

		string frameworkPath = "/Users/nairs77/Work/geBros/git/GB-Unity-Plugin/" + "Framework/";
		
		Process buildProcess = new Process();
		buildProcess.StartInfo.FileName = "python";
		buildProcess.StartInfo.Arguments = string.Format("/Users/nairs77/Work/geBros/git/GB-Unity-Plugin/Assets/GB/Editor/post_process.py \"{0}\" \"{1}\"", pathToBuildProject, frameworkPath);
		buildProcess.StartInfo.UseShellExecute = false;
		buildProcess.StartInfo.RedirectStandardOutput = false;
		
		buildProcess.Start();
		buildProcess.WaitForExit();
		UnityEngine.Debug.Log ("-- Custom PostprocessBuild -- Finished excute post process build phrase.");
	}

	/**
	 *  When using Xcode6 and Unity 4.3.4 or earlier, iOS build fail due to a missing include in CMVideoSampling.mm
	 *  Special Thank to robertcastle (http://gist.github.com/robertcastle)
	 
	public static void ReplaceOpenGL(string pathToBuildProject) {
		var dirInfo = Directory.GetFiles (pathToBuildProject, "CMVideoSampling.mm", SearchOption.AllDirectories);

		if (dirInfo == null || dirInfo.Length <= 0) {
			UnityEngine.Debug.LogError("Could not find CMVideoSampling.mm");
			return;
		}

		var cmSampleingPath = dirInfo [0];
		var content = new List<string> (File.ReadAllLines (cmSampleingPath));

		int index = 0;
		var doPatch = true;

		for (int i = 0; i <content.Count; ++i) {
			var line = content[i];

			if (line.Contains(_patchHeader)) {
				doPatch = false;
				break;
			}

			if (line.Contains(_oldHeader)) {
				index = i + 1;
			}
		}

		if (doPatch) {
			UnityEngine.Debug.Log ("Patching CMVideoSampling.mm");
			content.Insert(index, _patchHeader);
			File.WriteAllLines(cmSampleingPath, content.ToArray());
		} else {
			UnityEngine.Debug.Log ("CMVideoSampling.mm patch already applied. Skipping.");
		}
	}
	*/
	
	/**
	 *  Change App main 
	 */

	public static void ReplaceApplicationMain(string pathToBuildProject) {
		var searchDirInfo = Directory.GetFiles (pathToBuildProject, "main.mm", SearchOption.AllDirectories);

		if (searchDirInfo == null || searchDirInfo.Length <= 0) {
			UnityEngine.Debug.LogError("Could not find main.mm");
			return;
		}

		var mainFilePath = searchDirInfo [0];

		var content = new List<string> (File.ReadAllLines(mainFilePath));

		int index = 0;
		var doPatch = true;

		for (int i = 0; i <content.Count; ++i) {
			var line = content[i];

			if (line.Contains(_patchAppMain)) {
				doPatch = false;
				break;
			}

			if (line.Contains(_oldAppMain)) {
				index = i + 1;
			}
		}

		if (doPatch) {
			UnityEngine.Debug.Log ("Patching main.mm");
			content.Insert(index, _patchAppMain);
			content.RemoveAt(index-1);
			File.WriteAllLines(mainFilePath, content.ToArray());
		} else {
			UnityEngine.Debug.Log ("main.mm patch already applied. Skipping.");
		}

	}
}
#endif
