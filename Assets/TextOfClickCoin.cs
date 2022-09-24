using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOfClickCoin : MonoBehaviour
{
    [SerializeField] private protected Transform _selfTransformComponent;
    [SerializeField] private protected Text textOfCoinClick;

    private protected float deltaX, deltaY;
    private protected Vector3 _moveVector;
    private protected void Awake()
    {
        

       deltaX = UnityEngine.Random.Range(-1f, 1f);
        deltaY = UnityEngine.Random.Range(-1f, 1f);
        _moveVector = new Vector3(deltaX, deltaY);

        textOfCoinClick.text = string.Format("+{0}", HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStatOfFieldPlace[(int)Prefab_Part.Bonus.ClickCoin].Value);
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
