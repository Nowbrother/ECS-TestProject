using Unity.Entities;
using UnityEngine;
public class Sample_PositionAuthoring : MonoBehaviour { }
public class Sample_PositionBaker : Baker<Sample_PositionAuthoring>
{
    public override void Bake(Sample_PositionAuthoring authoring)
    {
        Entity entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
        AddComponent(entity, new Sample_PositionComponent
        {
            InitPosition = authoring.gameObject.transform.position,
            StartPosition = authoring.gameObject.transform.position,
            EndPosition = authoring.gameObject.transform.position,
            AccumTime = 1f,
            MoveTime = 1f,
        });
    }
}