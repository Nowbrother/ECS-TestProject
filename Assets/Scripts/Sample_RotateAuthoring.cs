using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
public class Sample_RotateAuthoring : MonoBehaviour
{
    public float RotateSpeed;
}
public class Sample_RotateBaker : Baker<Sample_RotateAuthoring>
{
    public override void Bake(Sample_RotateAuthoring authoring)
    {
        Entity entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
        AddComponent(entity, new Sample_RotateComponent
        {
            RotateSpeed = authoring.RotateSpeed,
            RotateCount = 0f
        });
        AddComponent(entity, new PostTransformMatrix
        {
            Value = new float4x4(authoring.transform.rotation, authoring.transform.localScale)
        });
    }
}