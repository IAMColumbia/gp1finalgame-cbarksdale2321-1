using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Referenced from inScope Studios(Singleton Pattern)
public abstract class ReferenceClass<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }
}
