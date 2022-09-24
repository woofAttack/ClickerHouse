using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{

    [SerializeField] private protected Sprite[] _spritesForChange;

    private protected Image _imageComponentOfGameObject;
    private protected int _countSprites;

    private void Awake()
    {
        _countSprites = _spritesForChange.Length;

        if (TryGetComponent<Image>(out Image imageComponent))
        {
            _imageComponentOfGameObject = imageComponent;
        }
        else
        {
            Debug.LogWarning(string.Format("Game Object {0} not have Image!", gameObject.name));
        }
    }

    public void __ChangeSprite(int numberSprite)
    {
        if (numberSprite < _countSprites)
        {
            _imageComponentOfGameObject.sprite = _spritesForChange[numberSprite];
        }
        else
        {
            Debug.LogWarning(string.Format("Game Object {0} not have sprite {1} number! It is have 0-{2}'n sprites.", gameObject.name, numberSprite, _countSprites - 1));
        }
    }

}
