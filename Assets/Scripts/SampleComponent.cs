using Unity.Entities;
public struct Sample_PositionComponent : IComponentData
{
    public float AccumTime;
    public float MoveTime;
    public Unity.Mathematics.float3 InitPosition;
    public Unity.Mathematics.float3 StartPosition;
    public Unity.Mathematics.float3 EndPosition;
}
public struct Sample_RotateComponent : IComponentData
{
    public float RotateSpeed;
    public float RotateCount;
}
public struct Sample_ScaleComponent : IComponentData
{
    public float AccumTime;
    public float ScaleTime;
    public Unity.Mathematics.float3 StartScale;
    public Unity.Mathematics.float3 EndScale;
}