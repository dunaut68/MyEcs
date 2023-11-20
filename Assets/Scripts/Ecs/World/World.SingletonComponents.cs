using System;
using System.Collections.Generic;

namespace Ecs
{
    using Entity = System.UInt32;

    public partial class World
    {
        private Dictionary<int, ISingletonComponent> _singletonComponents;

        public void AddSingletonComponent<T>() where T : ISingletonComponent, new()
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (_singletonComponents.ContainsKey(hashCode))
            {
                return;
            }

            _singletonComponents.Add(hashCode, new T());
        }

        public void AddSingletonComponent<T>(T sourceComponent) where T : ISingletonComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (_singletonComponents.ContainsKey(hashCode))
            {
                return;
            }

            _singletonComponents.Add(hashCode, sourceComponent);
        }

        public void RemoveSingletonComponent<T>() where T : ISingletonComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_singletonComponents.ContainsKey(hashCode))
            {
                return;
            }

            _singletonComponents.Remove(hashCode);
        }

        public T GetSingletonComponent<T>() where T : ISingletonComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_singletonComponents.TryGetValue(hashCode, out ISingletonComponent component))
            {
                return default;
            }

            return (T)component;
        }

        public void SetSingletonComponent<T>(T component) where T : ISingletonComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_singletonComponents.ContainsKey(hashCode))
            {
                return;
            }

            _singletonComponents[hashCode] = component;
        }
    }
}
