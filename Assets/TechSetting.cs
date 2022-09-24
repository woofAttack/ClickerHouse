using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechSetting : MonoBehaviour
{

    public static float modifScale;
    public static bool isHouseScreen;


    [SerializeField] private GameObject UI_HouseScreen;

    private void MoveImage(RectTransform rectTransformImage, float distance)
    {
        float b = rectTransformImage.offsetMin.y;
        
        rectTransformImage.offsetMin = new Vector3(0, distance);
        rectTransformImage.offsetMax = new Vector3(0, rectTransformImage.offsetMax.y + distance - b);
    }

    private void MoveHeightImage(RectTransform rectTransformImage, float height)
    {
        rectTransformImage.offsetMax = new Vector3(0, height + rectTransformImage.offsetMin.y);
    }

    private void Awake()
    {

        modifScale = Screen.width / 480f;
        //UI_HouseScreen.transform.position = new Vector3(0, -19f); 
        //или же -129 по позиции y
        Debug.Log(modifScale);
        //UI_HouseScreen.GetComponent<RectTransform>().offsetMin = new Vector3(0, -129);
        //UI_HouseScreen.GetComponent<RectTransform>().offsetMax = new Vector3(0, -123);

        //MoveImage(UI_HouseScreen.GetComponent<RectTransform>(), -129);

    }

}
