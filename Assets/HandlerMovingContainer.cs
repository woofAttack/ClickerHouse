using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerMovingContainer : MonoBehaviour
{
    [SerializeField] private protected float _paddingLeft;
    [SerializeField] private protected float _sizeOfContainer;



    private protected RectTransform _selfRectTransformComponent;

    [SerializeField] private protected List<ControlMovingContainer> _selfControlMovingContainers;

    private protected float _sizeOfContentView;
    private protected bool _isInistate;



    /// <summary>
    /// As Awake
    /// </summary>
    private void Inistate()
    {
        _selfRectTransformComponent = GetComponent<RectTransform>();
        if (_selfControlMovingContainers == null) _selfControlMovingContainers = new List<ControlMovingContainer>();
        _sizeOfContentView = 7f;

        _isInistate = true;
    }

    /// <summary>
    /// Add ControlMovingContainer and return count ListControlMovingContainers of Handler
    /// </summary>
    public int AddControlMovingContainer(ControlMovingContainer controlMovingContainer)
    {
        if (!_isInistate) Inistate();

        _selfControlMovingContainers.Add(controlMovingContainer);

        //_sizeOfContentView += 157f;
        //_selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView, 0f);

        return _selfControlMovingContainers.Count - 1;
    }

    public void MoveContainersRight(int numberEventOfControlMovingContainers)
    {
        _sizeOfContentView += 203;
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView + _selfRectTransformComponent.offsetMin.x, 0f);

        for (int i = numberEventOfControlMovingContainers + 1, imax = _selfControlMovingContainers.Count; i < imax; i++)
        {
            _selfControlMovingContainers[i].MoveContainerRight();
        }
    }

    public void MoveContainersLeft(int numberEventOfControlMovingContainers)
    {
        _sizeOfContentView -= 203;
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView + _selfRectTransformComponent.offsetMin.x, 0f);

        for (int i = numberEventOfControlMovingContainers + 1, imax = _selfControlMovingContainers.Count; i < imax; i++)
        {
            _selfControlMovingContainers[i].MoveContainerLeft();
        }
    }

    private void OnDisable()
    {
        if (!_isInistate) Inistate();

        _sizeOfContentView = 157f * _selfControlMovingContainers.Count;

        _selfRectTransformComponent.offsetMin = new Vector2(0f, -150f);
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView, 0f);
    }

    public void MoveTruePosition()
    {
        if (!_isInistate) Inistate();

        _sizeOfContentView = 157f * _selfControlMovingContainers.Count;

        _selfRectTransformComponent.offsetMin = new Vector2(0f, -155f);
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView + 9f, 0f);
    }

    public void MoveFalsePosition()
    {
        if (!_isInistate) Inistate();

        _sizeOfContentView = 157f * _selfControlMovingContainers.Count;

        Debug.Log("I MOVEEEEEEEEEEEEEEEE");
        _selfRectTransformComponent.offsetMin = new Vector2(0f, 0f);
        

        for (int i = 0, imax = _selfControlMovingContainers.Count; i < imax; i++)
        {
            _selfControlMovingContainers[i].ReturnPosition();
        }
    }


}
