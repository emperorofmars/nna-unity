#if NNA_AVA_UNIVRM0_FOUND
#if UNITY_EDITOR

using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using nna.ava.common;
using nna.processors;
using UnityEditor;
using UnityEngine;
using VRM;

namespace nna.ava.univrm0
{
	public class UNIVRM0VisemesBlendshapes : IGlobalProcessor
	{
		public const string _Type = "ava.voice_visemes_blendshape";
		public string Type => _Type;

		public void Process(NNAContext Context)
		{
			var Json = Context.GetJsonComponent(Context.Root.transform, _Type);
			
			Context.AddTask(new Task(() => {
				if(!Context.Root.TryGetComponent<VRMBlendShapeProxy>(out var vrmBlendshapeProxy)) return;

				SkinnedMeshRenderer smr = Utils.FindMainMesh(Context.Root.transform, (string)Json["meshinstance"]);
				if(!smr) return;

				var mappings = VisemeBlendshapeMapping.Map(smr);
				if(mappings.Count < 5) return;

				if(mappings.ContainsKey("aa"))
				{
					var clip = BlendshapeClipUtil.CreateSimple(Context, BlendShapePreset.A, smr, mappings["aa"]);
					vrmBlendshapeProxy.BlendShapeAvatar.Clips.Add(clip);
					Context.AddObjectToAsset(clip.name, clip);
				}

				if(mappings.ContainsKey("e"))
				{
					var clip = BlendshapeClipUtil.CreateSimple(Context, BlendShapePreset.E, smr, mappings["e"]);
					vrmBlendshapeProxy.BlendShapeAvatar.Clips.Add(clip);
					Context.AddObjectToAsset(clip.name, clip);
				}

				if(mappings.ContainsKey("ih"))
				{
					var clip = BlendshapeClipUtil.CreateSimple(Context, BlendShapePreset.I, smr, mappings["ih"]);
					vrmBlendshapeProxy.BlendShapeAvatar.Clips.Add(clip);
					Context.AddObjectToAsset(clip.name, clip);
				}

				if(mappings.ContainsKey("oh"))
				{
					var clip = BlendshapeClipUtil.CreateSimple(Context, BlendShapePreset.O, smr, mappings["oh"]);
					vrmBlendshapeProxy.BlendShapeAvatar.Clips.Add(clip);
					Context.AddObjectToAsset(clip.name, clip);
				}

				if(mappings.ContainsKey("ou"))
				{
					var clip = BlendshapeClipUtil.CreateSimple(Context, BlendShapePreset.U, smr, mappings["ou"]);
					vrmBlendshapeProxy.BlendShapeAvatar.Clips.Add(clip);
					Context.AddObjectToAsset(clip.name, clip);
				}
			}));
		}
	}

	[InitializeOnLoad]
	public class Register_UNIVRM0VisemesBlendshapes
	{
		static Register_UNIVRM0VisemesBlendshapes()
		{
			NNARegistry.RegisterGlobalProcessor(new UNIVRM0VisemesBlendshapes(), UNIVRM0VisemesBlendshapes._Type, DetectorUNIVRM0.NNA_UNIVRM0_CONTEXT);
			NNARegistry.RegisterIgnoredJsonType("ava.voice_visemes_blendshape", DetectorUNIVRM0.NNA_UNIVRM0_CONTEXT);
		}
	}
}

#endif
#endif
