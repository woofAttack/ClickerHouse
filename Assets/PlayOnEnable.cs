using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnEnable : MonoBehaviour
{
    [SerializeField] private protected Animation _selfAnimationComponentOfGameObject;

    private protected void OnEnable()
    {
        _selfAnimationComponentOfGameObject.Play();
    }

}
