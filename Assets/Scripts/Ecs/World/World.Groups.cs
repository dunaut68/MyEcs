using System;
using System.Collections.Generic;

namespace Ecs
{
    public partial class World
    {
        private readonly Group _group;
        public Group Group => _group;

        private readonly Dictionary<int, HashSet<Group>> _componentTypeToGroups;
        private bool _entityChanged;
        private readonly HashSet<int> _changedComponentTypes;
        private readonly List<Group> _waitingUpdatingGroups;

        public void SetTrackedComponentType<T>(Group group) where T : IComponent
        {
            Type componentType = typeof(T);
            int hashCode = componentType.GetHashCode();

            if (!_componentTypeToGroups.TryGetValue(hashCode, out HashSet<Group> groups))
            {
                _componentTypeToGroups.Add(hashCode, groups = new HashSet<Group>());
            }

            if (groups.Contains(group))
            {
                return;
            }

            groups.Add(group);
        }

        private void RefilterGroups()
        {
            // 实体发生变化时, 更新所有的组
            if (_entityChanged)
            {
                FilterRecursively(_group);
                _entityChanged = false;
                _changedComponentTypes.Clear();
                return;
            }

            // 组件发生变化时，按组件类型更新相关的组
            if (_changedComponentTypes.Count > 0)
            {
                // 收集待更新的组
                _waitingUpdatingGroups.Clear();
                foreach (int componentTypeHashCode in _changedComponentTypes)
                {
                    if (_componentTypeToGroups.TryGetValue(componentTypeHashCode, out HashSet<Group> groups))
                    {
                        foreach (Group group in groups)
                        {
                            _waitingUpdatingGroups.Add(group);
                        }
                    }
                }
                // 按照深度排序
                _waitingUpdatingGroups.Sort((a, b) => a.Depth - a.Depth);
                // 为了避免组的重复更新，剔除在同一路径下的两结点中深度较大的
                for (int i = _waitingUpdatingGroups.Count - 1; i >= 0; --i)
                {
                    for (int j = i - 1; j >= 0; --j)
                    {
                        if (_waitingUpdatingGroups[i].Depth > _waitingUpdatingGroups[j].Depth)
                        {
                            Group temp = _waitingUpdatingGroups[i];
                            while (temp.Parent != null && temp.Parent != _waitingUpdatingGroups[j])
                            {
                                temp = temp.Parent;
                            }
                            if (temp.Parent != null && temp.Parent == _waitingUpdatingGroups[j])
                            {
                                _waitingUpdatingGroups.RemoveAt(i);
                            }
                        }
                    }
                }

                _group.Filter();
                for (int i = 0; i < _waitingUpdatingGroups.Count; ++i)
                {
                    FilterRecursively(_waitingUpdatingGroups[i]);
                }

                _changedComponentTypes.Clear();
                return;
            }
        }

        private void FilterRecursively(Group group)
        {
            group.Filter();

            foreach (Group child in group.Children.Values)
            {
                FilterRecursively(child);
            }
        }
    }
}
