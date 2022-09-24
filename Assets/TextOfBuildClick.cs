using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOfBuildClick : MonoBehaviour
{
    [SerializeField] private protected Transform _selfTransformComponent;
    [SerializeField] private protected Text textOfBuildPointClick;

    private protected float deltaX, deltaY;
    private protected Vector3 _moveVector;
    private protected void Awake()
    {
        deltaX = UnityEngine.Random.Range(-1f, 1f);
        deltaY = UnityEngine.Random.Range(-1f, 1f);
        _moveVector = new Vector3(deltaX, deltaY);

        textOfBuildPointClick.text = string.Format("+{0}", Player.GetPowerOfHammer);
    }

    private protected void Update()
    {
        _selfTransformComponent.Translate(_moveVector * Time.deltaTime * 5f);
    }

    public void DestroyComponent()
    {
        Destroy(this.gameObject);
    }
}
