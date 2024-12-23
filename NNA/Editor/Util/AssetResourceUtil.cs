#if UNITY_EDITOR

using System.IO;
using System.Linq;
using UnityEditor;

namespace nna.util
{
	public static class AssetResourceUtil
	{
		public static T FindAsset<T>(string Match, bool AssetsOnly = true, string Suffix = null) where T : UnityEngine.Object
		{
			if(Suffix != null && !Suffix.StartsWith('.')) Suffix = '.' + Suffix;
			var resultPaths = AssetDatabase.FindAssets(Match)
				.Select(guid => AssetDatabase.GUIDToAssetPath(guid))
				.Where(r => (Suffix == null || r.ToLower().EndsWith(Suffix)) && (!AssetsOnly || r.StartsWith("Assets/")))
				.OrderBy(r => Path.GetFileNameWithoutExtension(r).Length);

			if(resultPaths.Count() > 0 && resultPaths.First() is var path)
			{
				return AssetDatabase.LoadAssetAtPath<T>(path);
			}
			else return null;
		}
	}

}

#endif
