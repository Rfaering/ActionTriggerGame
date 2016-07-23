using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class CachedGameObject
    {
        private readonly string _path;
        public CachedGameObject( string path )
        {
            _path = path;
        }

        private GameObject _cachedGameObject;

        public GameObject Get()
        {
            if( _cachedGameObject == null )
            {
                _cachedGameObject = GameObject.Find(_path);
            }
            return _cachedGameObject;
        }
    }
}
