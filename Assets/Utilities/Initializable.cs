using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializable : MonoBehaviour
{
    private bool isInitialized = false;

    protected virtual void Awake()
    {
        EnsureInitialized();
    }

    protected void EnsureInitialized()
    {
        if (isInitialized)
            return;
        Initialize();
    }

    protected virtual void Initialize()
    {
        isInitialized = true;
    }
}
