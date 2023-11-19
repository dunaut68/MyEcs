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

            _systems = new Dictionary<int, ISystem>();
            _updateOrders = new List<int>();

            _group = new Group(this);
        }
    }
}
