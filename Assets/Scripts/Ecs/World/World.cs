using System.Collections.Generic;

namespace Ecs
{
    using Entity = System.UInt32;

    public partial class World
    {
        public World()
        {
            _entities = new HashSet<Entity>();
            _recycledEntities = new Queue<Entity>();
            _maxEntity = Entity.MaxValue;
            _createdMaxEntity = 0;

            _components = new Dictionary<int, Dictionary<Entity, IComponent>>();
            _singletonComponents = new Dictionary<int, ISingletonComponent>();

            _systems = new Dictionary<int, ISystem>();
            _updateOrders = new List<int>();

            _group = new Group(this);
            _componentTypeToGroups = new Dictionary<int, HashSet<Group>>();
            _entityChanged = false;
            _changedComponentTypes = new HashSet<int>();
            _waitingUpdatingGroups = new List<Group>();
        }

        public void OnUpdate(int deltaTime)
        {
            for (int i = 0; i < _updateOrders.Count; ++i)
            {
                RefilterGroups();
                _systems[_updateOrders[i]].OnUpdate(deltaTime);
            }
        }
    }
}
