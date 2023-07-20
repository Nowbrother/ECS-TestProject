using Unity.Entities;
using UnityEngine;

public class Sample_ScaleAuthoring : MonoBehaviour { }
public class Sample_ScaleBaker : Baker<Sample_ScaleAuthoring>
{
    public override void Bake(Sample_ScaleAuthoring authoring)
    {
        Entity entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
        AddComponent(entity, new Sample_ScaleComponent
        {
            StartScale = authoring.gameObject.transform.localScale,
            EndScale = authoring.gameObject.transform.localScale,
            AccumTime = 1f,
            ScaleTime = 1f,
        });
        AddComponent(entity, new Unity.Transforms.PostTransformMatrix
        {
            Value = Unity.Mathematics.float4x4.Scale(authoring.transform.localScale)
        });
    }
}
