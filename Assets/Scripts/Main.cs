using Ecs;
using UnityEngine;

using Entity = System.UInt32;

public class Main : MonoBehaviour
{
    private World _world;

    private void Start()
    {
        _world = new World();

        for (int i = 0; i < 5000; ++i)
        {
            CreateCube();
        }

        _world.RegisterSystem<RotateSystem>();
    }

    private void Update()
    {
        _world.OnUpdate((int)(Time.deltaTime * 1000));
    }

    private void CreateCube()
    {
        Entity entity = _world.CreateEntity();

        _world.AddComponent(entity, new TransformComponent(new Vector3D((int)(Random.Range(-10f, 10f) * 1000), (int)(Random.Range(-10f, 10f) * 1000), (int)(Random.Range(-10f, 10f) * 1000))));

        GameObject cubeGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubeGameObject.name = $"Entity[{entity}]";
        _world.AddComponent(entity, new PrefabComponent() { prefab = cubeGameObject });
    }
}
