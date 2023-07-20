using Unity.Entities;
using UnityEngine;

public class RandomAuthoring : MonoBehaviour { }
public class RandomBaker : Baker<RandomAuthoring>
{
    public override void Bake(RandomAuthoring authoring)
    {
        Entity entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
        RandomComponent component = new RandomComponent
        {
            RandomCreater = new Unity.Mathematics.Random(1)
        };
        component.RandomCreater.NextFloat(-10f, 10f);
        AddComponent(entity, component);
    }
}
