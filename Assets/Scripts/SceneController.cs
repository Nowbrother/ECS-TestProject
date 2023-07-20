using Unity.Entities;
using Unity.Scenes;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public SubScene[] SubScenes;
    private Entity SceneEntity = Entity.Null;
    private int SelectedIndex = 2;
    private WorldUnmanaged UnmanagedWorld => World.DefaultGameObjectInjectionWorld.Unmanaged;
    private void Start()
    {
        SelectedIndex = 2;
        SceneEntity = SceneSystem.LoadSceneAsync(UnmanagedWorld, SubScenes[SelectedIndex].SceneGUID);
    }
    private void Update()
    {
        int selectIndex = SelectedIndex;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectIndex = 2;
        }

        if (selectIndex != SelectedIndex &&
            SceneEntity != Entity.Null &&
            SceneSystem.IsSceneLoaded(UnmanagedWorld, SceneEntity))
        {
            SceneSystem.UnloadScene(UnmanagedWorld, SceneEntity);
            SelectedIndex = selectIndex;
            SceneEntity = SceneSystem.LoadSceneAsync(UnmanagedWorld, SubScenes[SelectedIndex].SceneGUID);
        }
    }
}
