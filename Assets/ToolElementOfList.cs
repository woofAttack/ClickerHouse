using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolElementOfList : MonoBehaviour
{

    [SerializeField] private Image imageComponentOfElementList;
    [SerializeField] private Image imageComponentOfItemElementList;

    [SerializeField] private Prefab_Part iBlock;

    private Sprite imageItemOfElementList;

    public void SetElementPropity(Prefab_Part block)
    {
        iBlock = block;

        imageItemOfElementList = block.MainSprite;
        imageComponentOfItemElementList.sprite = block.MainSprite;
    }


    public void ButtonForChoose()
    {
        FieldPlace.currentZoomedFieldPlace.SetCurrentBuild(iBlock);
        FieldPlace.currentZoomedFieldPlace.CurrentSpriteRendererOfPart.sprite = imageItemOfElementList;
    }

}
