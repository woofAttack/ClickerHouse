using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ListImageForContainer
{
    [SerializeField] private protected Image _mainSquare;
    [SerializeField] private protected Image _backgroundOfPanelProgression;
    [SerializeField] private protected Image _backgroundOfPanelStat;
    [SerializeField] private protected Image _backgroundOfPanelButton;
    [SerializeField] private protected Image _panelOfNameOfPart;
    [SerializeField] private protected Image _panelOfLevelOfPart;

    [SerializeField] private protected Image _imageOfPartBlockOrRoof;
    [SerializeField] private protected Image _imageOfPartField;


    public Image mainSquare { get => _mainSquare; }
    public Image backgroundOfPanelProgression { get => _backgroundOfPanelProgression; }
    public Image backgroundOfPanelStat { get => _backgroundOfPanelStat; }
    public Image backgroundOfPanelButton { get => _backgroundOfPanelButton; }
    public Image panelOfNameOfPart { get => _panelOfNameOfPart; }
    public Image panelOfLevelOfPart { get => _panelOfLevelOfPart; }
    public Image imageOfPartBlockOrRoof { get => _imageOfPartBlockOrRoof; }
    public Image imageOfPartField { get => _imageOfPartField; }


}
