using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementOfList : MonoBehaviour
{

    [SerializeField] private Sprite[] imageOfElementList;
    private Sprite imageItemOfElementList;

    [SerializeField] private Image imageComponentOfElementList;
    [SerializeField] private Image imageComponentOfItemElementList;

    public enum Item
    {
        Block,
        Krysha,
        Field
    }

    private Item itemType;

    [SerializeField] private ButtonHouseScreen buttonHouseScreen;


    private SpriteRenderer linkObjectForSettingItem;

    public void Start()
    {

    }

    public void SetElementPropity(Sprite spriteItem, Item item)
    {

        imageItemOfElementList = spriteItem;
        imageComponentOfItemElementList.sprite = spriteItem;
        itemType = item;

        buttonHouseScreen = FindObjectOfType<ButtonHouseScreen>();
        //linkObjectForSettingItem = linkObject;

    }

    public void ButtonForSetItemOnHouse()
    {
        if (itemType == Item.Block)
        {
            buttonHouseScreen.renderBlock.sprite = imageItemOfElementList;
        }
        else if (itemType == Item.Krysha)
        {
            buttonHouseScreen.renderKrysha.sprite = imageItemOfElementList;
        }
        else
        {
            buttonHouseScreen.renderField.sprite = imageItemOfElementList;
        }
    }

}
