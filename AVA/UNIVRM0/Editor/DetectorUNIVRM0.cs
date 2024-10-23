#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;
using nna.util;

namespace nna.ava.univrm0
{
	[InitializeOnLoad, ExecuteInEditMode]
	public class DetectorUNIVRM0
	{
		const string NNA_AVA_UNIVRM0_FOUND = "NNA_AVA_UNIVRM0_FOUND";
		public const string NNA_UNIVRM0_CONTEXT = "univrm0";

		static DetectorUNIVRM0()
		{
			if(Directory.GetFiles(Path.GetDirectoryName(Application.dataPath), "IVRMComponent.cs", SearchOption.AllDirectories).Length > 0)
			{
				Debug.Log("AVA: Found UNIVRM0 SDK");
				ScriptDefinesManager.AddDefinesIfMissing(BuildTargetGroup.Standalone, NNA_AVA_UNIVRM0_FOUND);
			}
			else
			{
				Debug.Log("AVA: Didn't find UNIVRM0 SDK");
				ScriptDefinesManager.RemoveDefines(NNA_AVA_UNIVRM0_FOUND);
			}
		}
	}
}

#endif