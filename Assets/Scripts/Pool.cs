using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private int _poolSizePerObject;
    
    private Dictionary<MonoBehaviour, List<MonoBehaviour>> _objects;
    private Dictionary<MonoBehaviour, int> _pointers = new Dictionary<MonoBehaviour, int>();

    public static Pool Current => _current;
    private static Pool _current;

    private void Awake()
    {
        if (_current == null)
            _current = this;
        
        _objects = new Dictionary<MonoBehaviour, List<MonoBehaviour>>();
    }
    
    public void AddToPool(MonoBehaviour prefab)
    {
        AddToPool(prefab, _poolSizePerObject);
    }
    
    public void AddToPool(MonoBehaviour prefab, int poolSize)
    {
        if (_objects.ContainsKey(prefab))
        {
            return;
        }
        
        _objects.Add(prefab, new List<MonoBehaviour>());
        _pointers.Add(prefab, 0);

        for (var i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
        
            _objects[prefab].Add(obj);   
        }
    }

    [CanBeNull]
    public T Get<T>(T prefab) where T : MonoBehaviour
    {
        if (!_objects.ContainsKey(prefab))
        {
            Debug.LogError($"{prefab} wasn't pooled!");
            return null;
        }

        var pointer = _pointers[prefab] % _objects[prefab].Count;
        var obj = _objects[prefab][pointer++];
        
        _pointers[prefab] = pointer;
        
        obj.gameObject.SetActive(true);

        return (T) obj;
    }
}