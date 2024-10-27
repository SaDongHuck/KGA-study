using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance { get { return instance; } } // Get만 가능한 pubic properties.

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }
}
