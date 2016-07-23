using Assets.Scripts.World.Tile;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class CachedRelativeGameObject
    {
        private readonly string _path;
        private readonly MonoBehaviour _relativeTo;
        public CachedRelativeGameObject(MonoBehaviour relativeTo, string path)
        {
            _relativeTo = relativeTo;
            _path = path;
        }

        private GameObject _cachedGameObject;

        public GameObject Get()
        {
            if (_cachedGameObject == null)
            {
                _cachedGameObject = _relativeTo.transform.Find(_path).gameObject;
            }
            return _cachedGameObject;
        }
    }
}
