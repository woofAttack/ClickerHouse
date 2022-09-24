using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeRepeating : MonoBehaviour
{
    public UnityEvent onInvoke;
    public float repeatTime = 3f;
    public float startTime = 1f;

    void Start()
    {
        InvokeRepeating("Execute", startTime, repeatTime);
    }

    void Execute()
    {
        onInvoke.Invoke();
    }
}