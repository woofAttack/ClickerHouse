using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapSpritesOnClick : MonoBehaviour
{

    [SerializeField] private Sprite _spriteOnEnable;
    [SerializeField] private Sprite _spriteOnDisable;

    [SerializeField] private Image _selfImageComponent;

    private void Awake()
    {
        _selfImageComponent.sprite = _spriteOnDisable;
    }

    public void ChangeOnEnable()
    {
        _selfImageComponent.sprite = _spriteOnEnable;
    }

    public void ChangeOnDisable()
    {
        _selfImageComponent.sprite = _spriteOnDisable;
    }


}
