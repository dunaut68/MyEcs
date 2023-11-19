using UnityEngine;

namespace Ecs
{
    public class RotateSystem : ISystem
    {
        private World _world;
        public World World { set => _world = value; }

        public void OnUpdate(int deltaTime)
        {
            _world.Group.WithAll<TransformComponent, PrefabComponent>().ForEach((ref TransformComponent transformComponent, ref PrefabComponent prefabComponent) =>
            {
                transformComponent.rotation.y += (int)(360 * (float)deltaTime);
                prefabComponent.prefab.transform.localPosition = new Vector3((float)transformComponent.position.x / 1000, (float)transformComponent.position.y / 1000, (float)transformComponent.position.z / 1000);
                prefabComponent.prefab.transform.localRotation = Quaternion.Euler((float)transformComponent.rotation.x / 1000, (float)transformComponent.rotation.y / 1000, (float)transformComponent.rotation.z / 1000);
            });
        }
    }
}
