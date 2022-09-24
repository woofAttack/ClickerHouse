using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ListTextForContainer
{
    [SerializeField] private protected Text _textNameOfPart;
    [SerializeField] private protected Text _textLevelOfPart;
    [SerializeField] private protected Text _textCountOfBuildPoints;
    [SerializeField] private protected Text _textPrice;

    public Text textNameOfPart { get => _textNameOfPart; }
    public Text textLevelOfPart { get => _textLevelOfPart; }
    public Text textCountOfBuildPoints { get => _textCountOfBuildPoints; }
    public Text textPrice { get => _textPrice; }
}
