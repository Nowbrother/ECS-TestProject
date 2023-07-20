#if RUKHANKA_WITH_NETCODE

using Unity.Entities;
using Unity.NetCode;

/////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Rukhanka
{ 

[GhostComponentVariation(typeof(AnimatorControllerLayerComponent), "Animator Controller Layer")]
[GhostComponent()]
public struct AnimatorControllerLayerVariant
{
	[GhostField(SendData = false)]
	public BlobAssetReference<ControllerBlob> controller;
	[GhostField()]
	public int layerIndex;
	[GhostField()]
	public RuntimeAnimatorData rtd;
}

/////////////////////////////////////////////////////////////////////////////////////////////////////

}

#endif // RUKHANKA_WITH_NETCODE