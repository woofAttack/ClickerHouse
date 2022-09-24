using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldPlace : MonoBehaviour
{

    [SerializeField] private protected ElementOfListForWorkingState[] elementOfListForWorkingStates;

    public enum StateOfFieldPlace
    {
        NotWorking,
        Building,
        Working
    }

    public UnityEvent onClickToZoom;
    public UnityEvent onClickToEmpty;
    public UnityEvent onClickToBuilding;
    public UnityEvent onClickToWorking;

    [SerializeField] private float _positionX;
    [SerializeField] private float _positionY;

    public static float currentPositionX;
    public static float currentPositionY;
    public static FieldPlace currentZoomedFieldPlace;

    [SerializeField] private protected StateOfFieldPlace _stateOfFieldPlace;
    public StateOfFieldPlace GetStateOfFieldPlace { get => _stateOfFieldPlace; }

    public FieldPlace_Part[] _fieldPlace_Part;
    public FieldPlace_Part[] GetFieldPlace_Part { get => _fieldPlace_Part; }

    [SerializeField] private protected FieldPlace_Part _currentFieldPlace_Part;
    public FieldPlace_Part GetCurrentFieldPlace_Part { get => _currentFieldPlace_Part; }

    //public static Prefab_Part currentBlock;

    [SerializeField] private protected SpriteRenderer[] SpriteRendererOfPart;
    [System.NonSerialized] public SpriteRenderer CurrentSpriteRendererOfPart;

    [System.NonSerialized] public Prefab_Part selfBlock, selfRoof, selfField;
    [System.NonSerialized] public Prefab_Part currentPart1;
    [System.NonSerialized] public bool isHaveSelfBlock, isHaveSelfRoof, isHaveSelfField;

    public UnityEvent onStartBuild;
    public UnityEvent onBuild;
    public UnityEvent onEndBuild;


    public float currentBuild;
    [System.NonSerialized] public float maxCountBuild;

    [System.NonSerialized] public float countLevelBuilding;

    //[System.NonSerialized] public bool isBuilding;

    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _fieldPlace_Part[i].ElementOfListForWorking = elementOfListForWorkingStates[i];
        }
    }

    public void SetStaticOfCurrentField()
    {
        currentPositionX = _positionX;
        currentPositionY = _positionY;
        currentZoomedFieldPlace = this;
    }

    // Кнопка подтверждения для постройки
    public void SetCurrentBuild(Prefab_Part block)
    {
        switch (block.TypeOfPart)
        {
            case Prefab_Part.TypePart.Block: selfBlock = block; isHaveSelfBlock = true; break;
            case Prefab_Part.TypePart.Roof: selfRoof = block; isHaveSelfRoof = true; break;
            case Prefab_Part.TypePart.Field: selfField = block; isHaveSelfField = true; break;
        }

        currentPart1 = block;

        countLevelBuilding = block.Parts.Count;

        currentBuild = 0f;
        maxCountBuild = block.Parts[0].CountToBuild;

        _stateOfFieldPlace = StateOfFieldPlace.Building;

        CurrentSpriteRendererOfPart.color = new Color(1, 1, 1, 0);

        onStartBuild.Invoke();
    }

    public void SetCurrentBuild(BluePoint_Part bluePoint_PartForBuild)
    {
        int intTypePart = (int)bluePoint_PartForBuild.TypeOfPart;

        _currentFieldPlace_Part = _fieldPlace_Part[intTypePart];
        _currentFieldPlace_Part.SetBluepoint_Part(bluePoint_PartForBuild);

        _stateOfFieldPlace = StateOfFieldPlace.Building;


        countLevelBuilding = bluePoint_PartForBuild.CountLevelOfProgression;

        currentBuild = 0f;
        maxCountBuild = bluePoint_PartForBuild.countBuildPointToMax[0];

        CurrentSpriteRendererOfPart = SpriteRendererOfPart[intTypePart];
        CurrentSpriteRendererOfPart.color = new Color(1, 1, 1, 0);
        CurrentSpriteRendererOfPart.sprite = bluePoint_PartForBuild.BasePrefabPart.Parts[0].Sprite;

        onStartBuild.Invoke();
    }

    public void SetCurrentBuildForLeveling(FieldPlace_Part PartForBuild)
    {   
        _currentFieldPlace_Part = PartForBuild;


        _stateOfFieldPlace = StateOfFieldPlace.Building;
        _currentFieldPlace_Part.SetBuilding();

        currentBuild = 0f;
        maxCountBuild = _currentFieldPlace_Part.GetPart.countBuildPointToMax[_currentFieldPlace_Part.GetCurrentLevelProgression];

        CurrentSpriteRendererOfPart = SpriteRendererOfPart[(int)_currentFieldPlace_Part.GetPart.TypeOfPart];
        CurrentSpriteRendererOfPart.color = new Color(1, 1, 1, 0);
        CurrentSpriteRendererOfPart.sprite = _currentFieldPlace_Part.GetPart.BasePrefabPart.Parts[_currentFieldPlace_Part.GetCurrentLevelProgression].Sprite;

        onStartBuild.Invoke();
    }



    public void BuildPoint()
    {
        currentBuild++;
        CurrentSpriteRendererOfPart.color = new Color(1, 1, 1, currentBuild / maxCountBuild);

        onBuild.Invoke();

        if (currentBuild == maxCountBuild)
        {
            Debug.Log("Molodec");
            _stateOfFieldPlace = StateOfFieldPlace.NotWorking; //CheckState
            _currentFieldPlace_Part.UpLevelProgression();

            onEndBuild.Invoke();
        }


    }

    public void OnClickField()
    {
        if (!ToolsForFieldsHouses.isOpening)
        {
            if (!ToolsForFieldsHouses.isOpened)
            {
                SetStaticOfCurrentField();
                onClickToZoom.Invoke();
                //if (_stateOfFieldPlace == StateOfFieldPlace.Building) onStartBuild.Invoke();
            }
            else
            {
                if (this == currentZoomedFieldPlace)
                {
                    if (_stateOfFieldPlace == StateOfFieldPlace.Building)
                    {
                        BuildPoint();
                    }
                }
                //onClickToCloseField.Invoke();
            }
        }
    }
  
}

[System.Serializable]
public class FieldPlace_Part
{
    public enum StateOfFieldPlacePart
    {
        Empty,
        Building,
        Working
    }

    private BluePoint_Part _part;
    public BluePoint_Part GetPart { get => _part; }
    private Sprite _spriteOfPart;
    public Sprite GetSpriteOfPart { get => _spriteOfPart; }
    [SerializeField] private int currentLevelProgression;
    public int GetCurrentLevelProgression { get => currentLevelProgression; }

    public ElementOfListForWorkingState ElementOfListForWorking;

    [SerializeField] private StateOfFieldPlacePart _stateOfFieldPlacePart;



    public StateOfFieldPlacePart GetStateOfFieldPlacePart { get => _stateOfFieldPlacePart; }

    public void SetBluepoint_Part(BluePoint_Part bluePoint_Part)
    {
        _part = bluePoint_Part;
        _stateOfFieldPlacePart = StateOfFieldPlacePart.Building;
        currentLevelProgression = 0;
    }

    public void SetBuilding()
    {

        _stateOfFieldPlacePart = StateOfFieldPlacePart.Building;

    }

    public void UpLevelProgression()
    {
        currentLevelProgression++;
        _stateOfFieldPlacePart = StateOfFieldPlacePart.Working;
        //ElementOfListForWorking.SetCurrentBluePointPart(this);
    }
}
