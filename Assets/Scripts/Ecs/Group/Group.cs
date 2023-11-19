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
        private Group _parent;
        private Dictionary<int, Group> _children;
        public Dictionary<int, Group> Children => _children ??= new Dictionary<int, Group>();

        protected World _world;
        private HashSet<Entity> _entities;
        public HashSet<Entity> Entities => _entities;


        public Group(World world, Group parent = null)
        {
            _world = world;
            _parent = parent;
            _entities = new HashSet<Entity>();

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

        public void Refilter()
        {
            Filter();
            foreach (Group group in Children.Values)
            {
                group.Filter();
            }
        }

        protected virtual bool Match(Entity entity)
        {
            return true;
        }
    }
}
