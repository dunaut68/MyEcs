namespace Ecs
{
    public class TransformComponent : IComponent
    {
        public Vector3D position;
        public Vector3D rotation;

        public TransformComponent(Vector3D position = default, Vector3D rotation = default)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
}
