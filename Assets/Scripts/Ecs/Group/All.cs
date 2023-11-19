using System.Collections.Generic;

namespace Ecs
{
    using Entity = System.UInt32;

    public class All<T> : Group where T : IComponent
    {
        public All(World world, Group parent = null) : base(world, parent) { }

        protected override bool Match(Entity entity)
        {
            return _world.GetComponent<T>(entity) != null;
        }
    }

    public class All<T1, T2> : Group where T1 : IComponent where T2 : IComponent
    {
        public All(World world, Group parent = null) : base(world, parent) { }

        protected override bool Match(Entity entity)
        {
            return _world.GetComponent<T1>(entity) != null
                && _world.GetComponent<T2>(entity) != null;
        }
    }

    public class All<T1, T2, T3> : Group where T1 : IComponent where T2 : IComponent where T3 : IComponent
    {
        public All(World world, Group parent = null) : base(world, parent) { }

        protected override bool Match(Entity entity)
        {
            return _world.GetComponent<T1>(entity) != null
                && _world.GetComponent<T2>(entity) != null
                && _world.GetComponent<T3>(entity) != null;
        }
    }
}
