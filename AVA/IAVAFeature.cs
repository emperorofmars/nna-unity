
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace nna.ava
{
	public interface IAVAFeature
	{
		string Type {get;}
		bool AutoDetect(NNAContext Context, Component UnityComponent, JObject Json);
	}
}

