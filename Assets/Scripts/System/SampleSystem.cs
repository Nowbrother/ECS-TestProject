using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
//#if ECS_SAMPLE
[BurstCompile]
public partial struct SampleSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state) { }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (SystemAPI.HasSingleton<RandomComponent>() == false)
            return;

        float deltaTime = SystemAPI.Time.DeltaTime;

        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        JobHandle positionJob = new Sample_PositionJob
        {
            Random = randomComponent,
            DeltaTime = deltaTime,
        }.ScheduleParallel(state.Dependency);
        positionJob.Complete();

        JobHandle rotateJob = new Sample_RotateJob
        {
            DeltaTime = deltaTime
        }.ScheduleParallel(state.Dependency);
        rotateJob.Complete();

        JobHandle scaleJob = new Sample_ScaleJob
        {
            Random = randomComponent,
            DeltaTime = deltaTime,
        }.ScheduleParallel(state.Dependency);
        scaleJob.Complete();


        state.Dependency = positionJob;
        state.Dependency = rotateJob;
        state.Dependency = scaleJob;
    }
}
[BurstCompile]
public partial struct Sample_PositionJob : IJobEntity
{
    [Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
    public RefRW<RandomComponent> Random;
    public float DeltaTime;
    [BurstCompile]
    public void Execute(Entity entity, ref LocalTransform transform, ref Sample_PositionComponent component)
    {
        if (component.AccumTime >= component.MoveTime)
        {
            component.AccumTime = 0f;

            component.StartPosition = transform.Position;

            component.EndPosition = component.InitPosition +
                Random.ValueRW.RandomCreater.NextFloat3(math.down() * 2f, math.up() * 2f) +
                Random.ValueRW.RandomCreater.NextFloat3(math.left() * 2f, math.right() * 2f);

            component.MoveTime = math.distance(transform.Position, component.EndPosition) / 4f;

        }

        component.AccumTime += DeltaTime;

        transform.Position = math.lerp(
            component.StartPosition, component.EndPosition, component.AccumTime / component.MoveTime);
    }
}
[BurstCompile]
public partial struct Sample_RotateJob : IJobEntity
{
    public float DeltaTime;
    [BurstCompile]
    public void Execute(ref LocalTransform transform, ref Sample_RotateComponent component)
    {
        component.RotateCount += component.RotateSpeed * DeltaTime;
        if (component.RotateCount >= 360f)
            component.RotateCount -= 360f;
        transform = transform.RotateY(component.RotateSpeed * DeltaTime);
    }
}
[BurstCompile]
public partial struct Sample_ScaleJob : IJobEntity
{
    [Unity.Collections.LowLevel.Unsafe.NativeDisableUnsafePtrRestriction]
    public RefRW<RandomComponent> Random;
    public float DeltaTime;
    [BurstCompile]
    public void Execute(Entity entity, ref Sample_ScaleComponent component, ref PostTransformMatrix matrix)
    {

        if (component.AccumTime >= component.ScaleTime)
        {
            component.AccumTime = 0f;

            component.StartScale = matrix.Value.Scale();

            component.EndScale =
                math.right() * Random.ValueRW.RandomCreater.NextFloat(0.5f, 2f) +
                math.up() * Random.ValueRW.RandomCreater.NextFloat3(0.5f, 2f) +
                math.forward() * Random.ValueRW.RandomCreater.NextFloat3(0.5f, 2f);

            float dis = math.abs(component.StartScale.x - component.EndScale.x);

            if (dis < math.abs(component.StartScale.y - component.EndScale.y))
                dis = math.abs(component.StartScale.y - component.EndScale.y);

            if (dis < math.abs(component.StartScale.z - component.EndScale.z))
                dis = math.abs(component.StartScale.z - component.EndScale.z);

            component.ScaleTime = dis;
        }

        component.AccumTime += DeltaTime;

        matrix.Value = float4x4.Scale(
                math.lerp(component.StartScale, component.EndScale, component.AccumTime / component.ScaleTime));

    }
}
//#endif