using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ListStatForContainer
{
    [SerializeField] private protected Text _textStats;
    [SerializeField] private protected Image _imageStats;
    [SerializeField] private protected Image _backgroundStats;

    public Text textOfStat { get => _textStats; }
    public Image imageStats { get => _imageStats; }
    public Image backgroundStats { get => _backgroundStats; }

}
