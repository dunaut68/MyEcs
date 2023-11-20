using System.Collections.Generic;

namespace Ecs
{
    using Entity = System.UInt32;

    public enum GroupType
    {
        All,
        Any,
        None,
    }

    public partial class Group
    {
        protected World _world;
        private readonly HashSet<Entity> _entities;
        public HashSet<Entity> Entities => _entities;

        private Group _parent;
        public Group Parent => _parent;
        private Dictionary<int, Group> _children;
        public Dictionary<int, Group> Children => _children ??= new Dictionary<int, Group>();
        private int _depth;
        public int Depth => _depth;

        public Group(World world, Group parent = null)
        {
            _world = world;
            _entities = new HashSet<Entity>();

            _parent = parent;
            _depth = parent != null ? parent.Depth + 1 : 0;

            Filter();
        }

        public void Filter()
        {
            _entities.Clear();
            foreach (Entity entity in _parent != null ? _parent.Entities : _world.GetEntities())
            {
                if (Match(entity))
                {
                    _entities.Add(entity);
                }
            }
        }

        protected virtual bool Match(Entity entity)
        {
            return true;
        }
    }
}
