#if UNITY_EDITOR

using Newtonsoft.Json.Linq;
using UnityEngine;

namespace nna.ava.vrchat
{
	public interface IAVAFeatureVRC
	{
		string Type {get;}
		bool AutoDetect(NNAContext Context, Component UnityComponent, JObject Json);
	}
}

#endif