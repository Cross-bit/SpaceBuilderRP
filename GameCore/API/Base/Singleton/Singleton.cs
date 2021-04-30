using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T : Component 
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

    void Awake()
    {
        /*if (managerInstance == null)
        {
            managerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
