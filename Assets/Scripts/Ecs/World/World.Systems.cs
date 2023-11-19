using System;
using System.Collections.Generic;

namespace Ecs
{
    public partial class World
    {
        private Dictionary<int, ISystem> _systems;
        private List<int> _updateOrders;

        public void RegisterSystem<T>() where T : ISystem, new()
        {
            Type systemType = typeof(T);
            int hashCode = systemType.GetHashCode();

            if (_systems.ContainsKey(hashCode))
            {
                return;
            }

            T system = new T();
            system.World = this;
            _systems.Add(hashCode, system);
            _updateOrders.Add(hashCode);
        }

        public void CancelSystem<T>() where T : ISystem
        {
            Type systemType = typeof(T);
            int hashCode = systemType.GetHashCode();

            if (!_systems.ContainsKey(hashCode))
            {
                return;
            }

            _systems.Remove(hashCode);
            _updateOrders.Remove(hashCode);
        }

        public void OnUpdate(int deltaTime)
        {
            for (int i = 0; i < _updateOrders.Count; ++i)
            {
                _systems[_updateOrders[i]].OnUpdate(deltaTime);
            }
        }
    }
}
