using System;
using System.Collections.Generic;

namespace Ecs
{
    using Entity = System.UInt32;

    public partial class World
    {
        private Dictionary<int, Dictionary<Entity, IComponent>> _components;

        public void AddComponent<T>(Entity entity) where T : IComponent, new()
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_components.TryGetValue(hashCode, out Dictionary<Entity, IComponent> components))
            {
                _components.Add(hashCode, components = new Dictionary<Entity, IComponent>());
            }

            if (components.ContainsKey(entity))
            {
                return;
            }

            T component = new T();
            components.Add(entity, component);
            OnComponentAdded(entity, component);
        }

        public void AddComponent<T>(Entity entity, T sourceComponent) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_components.TryGetValue(hashCode, out Dictionary<Entity, IComponent> components))
            {
                _components.Add(hashCode, components = new Dictionary<Entity, IComponent>());
            }

            if (components.ContainsKey(entity))
            {
                return;
            }

            OnComponentAdded(entity, sourceComponent);
            components.Add(entity, sourceComponent);
        }

        public void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_components.TryGetValue(hashCode, out Dictionary<Entity, IComponent> components))
            {
                return;
            }

            if (!components.TryGetValue(entity, out IComponent component))
            {
                return;
            }

            OnComponentRemoving(entity, (T)component);
            components.Remove(entity);
        }

        public T GetComponent<T>(Entity entity) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_components.TryGetValue(hashCode, out Dictionary<Entity, IComponent> components))
            {
                return default;
            }

            if (!components.TryGetValue(entity, out IComponent component))
            {
                return default;
            }

            return (T)component;
        }

        public void SetComponent<T>(Entity entity, T component) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_components.TryGetValue(hashCode, out Dictionary<Entity, IComponent> components))
            {
                return;
            }

            if (!components.ContainsKey(entity))
            {
                return;
            }

            components[entity] = component;
        }

        private void OnComponentAdded<T>(Entity entity, T component) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();
            if (!_changedComponentTypes.Contains(hashCode))
            {
                _changedComponentTypes.Add(hashCode);
            }
        }

        private void OnComponentRemoving<T>(Entity entity, T component) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();
            if (!_changedComponentTypes.Contains(hashCode))
            {
                _changedComponentTypes.Add(hashCode);
            }
        }
    }
}
