using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _poolSizePerObject;
    
    private        Dictionary<string, List<object>> _objects;
    private        Dictionary<string, int>          _pointers       = new Dictionary<string, int>();
    private        HashSet<string>                  _isConcreteType = new HashSet<string>();
    public static  Pool                             Current => _current;
    private static Pool                             _current;

    private void Awake()
    {
        if (_current == null)
            _current = this;
        
        _objects = new Dictionary<string, List<object>>();
    }
    
    public void AddToPool(MonoBehaviour prefab)
    {
        AddToPool(prefab, _poolSizePerObject);
    }
    
    public void AddToPool(GameObject prefab)
    {
        AddToPool(prefab, _poolSizePerObject);
    }
    
    public void AddToPool(MonoBehaviour prefab, int poolSize)
    {
        var objKey = GetObjectKey(prefab);
        if (_objects.ContainsKey(objKey))
        {
            return;
        }
        
        _objects.Add(objKey, new List<object>());
        _pointers.Add(objKey, 0);

        for (var i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
        
            _objects[objKey].Add(obj);

            _isConcreteType.Add(objKey);
        }
    }
    
    public void AddToPool(GameObject prefab, int poolSize)
    {
        var objKey = GetObjectKey(prefab);
        if (_objects.ContainsKey(objKey))
        {
            return;
        }
        
        _objects.Add(objKey, new List<object>());
        _pointers.Add(objKey, 0);

        for (var i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
        
            _objects[objKey].Add(obj);   
        }
    }

    [CanBeNull]
    public T Get<T>(T prefab) where T : MonoBehaviour
    {
        var objKey = GetObjectKey(prefab);
        if (!_objects.ContainsKey(objKey))
        {
            Debug.LogError($"{objKey} wasn't pooled!");
            return null;
        }

        var pointer = _pointers[objKey] % _objects[objKey].Count;
        var obj = _objects[objKey][pointer++];
        
        _pointers[objKey] = pointer;

        if (!_isConcreteType.Contains(objKey)) return null;

        var concreteObj = (T) obj;
        concreteObj.gameObject.SetActive(true);

        return (T) obj;
    }
    
    [CanBeNull]
    public GameObject Get(GameObject prefab)
    {
        var objKey = GetObjectKey(prefab);
        if (!_objects.ContainsKey(objKey))
        {
            Debug.LogError($"{objKey} wasn't pooled!");
            return null;
        }

        var pointer = _pointers[objKey] % _objects[objKey].Count;
        var obj = _objects[objKey][pointer++] as GameObject;
        
        _pointers[objKey] = pointer;

        obj.SetActive(true);

        return obj;
    }

    private static string GetObjectKey(Object obj)
    {
        var objKey = obj.name + "-" + obj.GetInstanceID();
        return objKey;
    }
}