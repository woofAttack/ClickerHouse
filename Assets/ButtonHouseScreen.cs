using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHouseScreen : MonoBehaviour
{

    [SerializeField] private GameObject UI_HouseScreen;
    private bool isOpen = false;

    [SerializeField] private GameObject[] Content;
    [SerializeField] private GameObject GOElementOfList;

    public SpriteRenderer renderBlock, renderKrysha, renderField;

    


    

    private void MoveHeightImage(RectTransform rectTransformImage, float height)
    {
        rectTransformImage.offsetMax = new Vector3(0, height + rectTransformImage.offsetMin.y);
    }

    public void CreateMenu_Blocks(int i)
    {
        foreach(GameObject ContentItem in Content)
        {
            ContentItem.SetActive(false);
        }

        if (!isOpen)
        {
            Content[i].SetActive(true);
            MoveHeightImage(UI_HouseScreen.GetComponent<RectTransform>(), 135);
            isOpen = true;
        }
        else
        {
            MoveHeightImage(UI_HouseScreen.GetComponent<RectTransform>(), 6);
            isOpen = false;
        }
    }

}
