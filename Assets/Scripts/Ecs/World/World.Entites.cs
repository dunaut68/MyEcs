using System.Collections.Generic;

namespace Ecs
{
    using Entity = System.UInt32;

    public partial class World
    {
        private HashSet<Entity> _entities;
        private Queue<Entity> _recycledEntities;
        private Entity _maxEntity;
        private Entity _createdMaxEntity;

        public Entity CreateEntity()
        {
            if (_createdMaxEntity >= _maxEntity)
            {
                return _maxEntity;
            }

            if (!_recycledEntities.TryDequeue(out Entity entity))
            {
                entity = _createdMaxEntity++;
            }

            _entities.Add(entity);
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            if (!_entities.Contains(entity))
            {
                return;
            }

            _entities.Remove(entity);
        }

        public ISet<Entity> GetEntities()
        {
            return _entities;
        }
    }
}
