using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectElementWidth : MonoBehaviour
{
    [SerializeField] private protected GridLayoutGroup gridLayoutGroup;

    private void Start()
    {
        //gridLayoutGroup.cellSize = new Vector2(Screen.width / Mathf.Floor(TechSetting.modifScale), gridLayoutGroup.cellSize.y);
        gridLayoutGroup.cellSize = new Vector2(Screen.width / (float)System.Math.Round(TechSetting.modifScale, 1), gridLayoutGroup.cellSize.y);
    }

}
