using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class CachedGameObject<T>
    {
        private readonly string _path;
        public CachedGameObject( string path )
        {
            _path = path;
        }

        private T _cachedGameObject;

        public T Get()
        {
            if( _cachedGameObject == null )
            {
                _cachedGameObject = GameObject.Find(_path).GetComponent<T>();
            }
            return _cachedGameObject;
        }
    }
}
