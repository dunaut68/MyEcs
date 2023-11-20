using System;

namespace Ecs
{
    using Entity = UInt32;

    public delegate void Iterate<T>(ref T component) where T : IComponent;
    public delegate void Iterate<T1, T2>(ref T1 component1, ref T2 component2) where T1 : IComponent where T2 : IComponent;
    public delegate void Iterate<T1, T2, T3>(ref T1 component1, ref T2 component2, ref T3 component3) where T1 : IComponent where T2 : IComponent where T3 : IComponent;

    public partial class Group
    {
        protected int GetKey(GroupType groupType, params Type[] componentTypes)
        {
            HashCode hashCode = new HashCode();

            hashCode.Add(groupType);

            Array.Sort(componentTypes, (a, b) => b.GetHashCode() - a.GetHashCode());
            for (int i = 0; i < componentTypes.Length; ++i)
            {
                hashCode.Add(componentTypes[i]);
            }

            return hashCode.ToHashCode();
        }

        public Group WithAll<T>() where T : IComponent
        {
            int key = GetKey(GroupType.All, typeof(T));

            if (!Children.TryGetValue(key, out Group group))
            {
                Children.Add(key, group = new All<T>(_world, this));
                _world.SetTrackedComponentType<T>(group);
            }

            return group;
        }

        public Group WithAll<T1, T2>() where T1 : IComponent where T2 : IComponent
        {
            int key = GetKey(GroupType.All, typeof(T1), typeof(T2));

            if (!Children.TryGetValue(key, out Group group))
            {
                Children.Add(key, group = new All<T1, T2>(_world, this));
                _world.SetTrackedComponentType<T1>(group);
                _world.SetTrackedComponentType<T2>(group);
            }

            return group;
        }

        public Group WithAll<T1, T2, T3>() where T1 : IComponent where T2 : IComponent where T3 : IComponent
        {
            int key = GetKey(GroupType.All, typeof(T1), typeof(T2), typeof(T3));

            if (!Children.TryGetValue(key, out Group group))
            {
                Children.Add(key, group = new All<T1, T2, T3>(_world, this));
                _world.SetTrackedComponentType<T1>(group);
                _world.SetTrackedComponentType<T2>(group);
                _world.SetTrackedComponentType<T3>(group);
            }

            return group;
        }

        public void ForEach<T>(Iterate<T> iterate) where T : IComponent
        {
            foreach (Entity entity in _entities)
            {
                T component = _world.GetComponent<T>(entity);
                iterate(ref component);
            }
        }

        public void ForEach<T1, T2>(Iterate<T1, T2> iterate) where T1 : IComponent where T2 : IComponent
        {
            foreach (Entity entity in _entities)
            {
                T1 component1 = _world.GetComponent<T1>(entity);
                T2 component2 = _world.GetComponent<T2>(entity);
                iterate(ref component1, ref component2);
            }
        }

        public void ForEach<T1, T2, T3>(Iterate<T1, T2, T3> iterate) where T1 : IComponent where T2 : IComponent where T3 : IComponent
        {
            foreach (Entity entity in _entities)
            {
                T1 component1 = _world.GetComponent<T1>(entity);
                T2 component2 = _world.GetComponent<T2>(entity);
                T3 component3 = _world.GetComponent<T3>(entity);
                iterate(ref component1, ref component2, ref component3);
            }
        }
    }
}
