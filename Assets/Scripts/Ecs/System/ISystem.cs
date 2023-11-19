namespace Ecs
{
    public interface ISystem
    {
        World World { set; }

        void OnUpdate(int deltaTime);
    }
}
