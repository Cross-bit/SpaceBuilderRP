using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonPersistant<T> : MonoBehaviour where T : Component
{
    private static T _Instance;

    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<T>();
                if (_Instance == null)
                {
                    var obj = new GameObject();
                    obj.hideFlags = HideFlags.HideAndDontSave;
                    _Instance = obj.AddComponent<T>();
                }
            }
            return _Instance;
        }
        set
        {
            _Instance = value;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(this);
        if (_Instance == null)
        {
            _Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
