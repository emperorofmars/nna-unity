#if AVA_VRCSDK3_FOUND
#if UNITY_EDITOR

using System.Threading.Tasks;
using nna.ava.common;
using nna.processors;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace nna.ava.vrchat
{
	public class VRCVisemesBlendshapes : IGlobalProcessor
	{
		public const string _Type = "ava.voice_visemes_blendshape";
		public string Type => _Type;

		public void Process(NNAContext Context)
		{
			Context.AddTask(new Task(() => {
				var Json = Context.GetJsonComponent(Context.Root.transform, _Type);
				var avatar = Context.Root.GetComponent<VRCAvatarDescriptor>();

				SkinnedMeshRenderer smr = Utils.FindMainMesh(Context.Root.transform, (string)Json["meshinstance"]);
				if(!smr) return;
				
				var Mappings = VisemeBlendshapeMapping.Map(smr);
				if(Mappings.Count == 15)
				{
					Context.AddTask(new Task(() => {
						avatar.VisemeSkinnedMesh = smr;
						avatar.lipSync = VRC.SDKBase.VRC_AvatarDescriptor.LipSyncStyle.VisemeBlendShape;
						avatar.VisemeBlendShapes = new string[15];
						avatar.VisemeBlendShapes[0] = Mappings["sil"];
						avatar.VisemeBlendShapes[1] = Mappings["pp"];
						avatar.VisemeBlendShapes[2] = Mappings["ff"];
						avatar.VisemeBlendShapes[3] = Mappings["th"];
						avatar.VisemeBlendShapes[4] = Mappings["dd"];
						avatar.VisemeBlendShapes[5] = Mappings["kk"];
						avatar.VisemeBlendShapes[6] = Mappings["ch"];
						avatar.VisemeBlendShapes[7] = Mappings["ss"];
						avatar.VisemeBlendShapes[8] = Mappings["nn"];
						avatar.VisemeBlendShapes[9] = Mappings["rr"];
						avatar.VisemeBlendShapes[10] = Mappings["aa"];
						avatar.VisemeBlendShapes[11] = Mappings["e"];
						avatar.VisemeBlendShapes[12] = Mappings["ih"];
						avatar.VisemeBlendShapes[13] = Mappings["oh"];
						avatar.VisemeBlendShapes[14] = Mappings["ou"];
					}));
					return;
				}
			}));
		}
	}

	[InitializeOnLoad]
	public class Register_VRCVisemesBlendshapes
	{
		static Register_VRCVisemesBlendshapes()
		{
			NNARegistry.RegisterGlobalProcessor(new VRCVisemesBlendshapes(), VRCVisemesBlendshapes._Type, DetectorVRC.NNA_VRC_AVATAR_CONTEXT);
			NNARegistry.RegisterIgnoredJsonType(VRCVisemesBlendshapes._Type, DetectorVRC.NNA_VRC_AVATAR_CONTEXT);
		}
	}
}

#endif
#endif