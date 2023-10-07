using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public string textToPass;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
