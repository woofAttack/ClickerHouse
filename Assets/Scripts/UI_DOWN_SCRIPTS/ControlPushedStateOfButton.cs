using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlPushedStateOfButton : MonoBehaviour
{
    private protected bool _isEnable;

    private protected bool _isPushed;
    private protected static ControlPushedStateOfButton _currentPushedButton;

    public bool isPushed { get => _isPushed; }

    public UnityEvent OnPuched, OnUnpushedClicked, OnUnpushed;

    public UnityEvent OnEnabled, OnDisabled;


    private void Awake()
    {
        _isEnable = true;
    }

    public void __ClickOnButton()
    {
        //Debug.Log(string.Format("{0} with IsEnable = {1}", gameObject.name, _isEnable));

        if (_isEnable)
        {
            if (_currentPushedButton == null)
            {
                _currentPushedButton = this;
                _isPushed = true;

                OnPuched.Invoke();
            }
            else
            {
                if (_isPushed)
                {
                    _currentPushedButton = null;
                    _isPushed = false;

                    OnUnpushedClicked.Invoke();
                }
                else
                {
                    _currentPushedButton._isPushed = false;
                    _currentPushedButton.OnUnpushed.Invoke();

                    _currentPushedButton = this;
                    _isPushed = true;

                    OnPuched.Invoke();
                }
            }
        }
    }

    public void __SetEnable()
    {
        _isEnable = true;
        OnEnabled.Invoke();
    }

    public void __SetDisable()
    {
        _isEnable = false;
        _isPushed = false;
        OnDisabled.Invoke();
    }

    public void __UpdateButton()
    {
        if (_isPushed) OnPuched.Invoke();
    }

    public void __DropPush()
    {
        _currentPushedButton = null;
        _isPushed = false;

        OnUnpushedClicked.Invoke();
    }

}
