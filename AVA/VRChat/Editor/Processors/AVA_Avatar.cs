#if UNITY_EDITOR
#if NNA_AVA_VRCSDK3_FOUND

using nna.processors;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using System.Linq;
using UnityEditor;

namespace nna.ava.vrchat
{
	public class AVA_Avatar_VRCProcessor : IGlobalProcessor
	{
		public const string _Type = "ava.avatar";
		public string Type => _Type;
		public const uint _Order = 1;
		public uint Order => _Order;

		public void Process(NNAContext Context)
		{
			var avatar = AVAVRCUtils.InitAvatarDescriptor(Context);
			
			var avatarComponentJson = Context.GetComponent(Context.Root.transform, "ava.avatar");
			if(avatarComponentJson.ContainsKey("id")) avatar.name = "$nna:" + (string)avatarComponentJson["id"];
			
			if(Context.Root.GetComponent<VRCAvatarDescriptor>() == null)
			{
				AVAVRCUtils.GetOrInitAnimator(Context);
			}
		}
	}

	public static class AVAVRCUtils
	{
		public static VRCAvatarDescriptor InitAvatarDescriptor(NNAContext Context)
		{
			var avatar = Context.Root.AddComponent<VRCAvatarDescriptor>();
			
			// set viewport
			if(Context.Root.transform.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == "ViewportFirstPerson") is var viewportNode && viewportNode != null)
			{
				avatar.ViewPosition = viewportNode.transform.position - Context.Root.transform.position;
				Context.AddTrash(viewportNode);
			}

			return avatar;
		}

		public static Animator GetOrInitAnimator(NNAContext Context)
		{
			if(!Context.Root.TryGetComponent<Animator>(out var animator))
			{
				animator = Context.Root.AddComponent<Animator>();
			}
			animator.applyRootMotion = true;
			animator.updateMode = AnimatorUpdateMode.Normal;
			animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;

			return animator;
		}
	}

	[InitializeOnLoad]
	public class Register_AVA_Avatar_VRC
	{
		static Register_AVA_Avatar_VRC()
		{
			NNARegistry.RegisterGlobalProcessor(new AVA_Avatar_VRCProcessor(), DetectorVRC.NNA_VRC_AVATAR_CONTEXT, true);
		}
	}
}

#endif
#endif