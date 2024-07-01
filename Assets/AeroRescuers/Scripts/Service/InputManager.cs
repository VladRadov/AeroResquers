using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour
{
    private InputMap _inputMap;
    private bool _mouseHold;

    public ReactiveCommand OnMoveCommand = new();

    private void Awake()
    {
        _mouseHold = false;
        _inputMap = new InputMap();
        _inputMap.Map.Move.performed += (context) => { _mouseHold = true; };
        _inputMap.Map.Move.canceled += (context) => { _mouseHold = false; };
    }

    private async void OnHoldMouseButton()
    {
        while (_mouseHold)
        {
            OnMoveCommand.Execute();
            await Task.Delay(1000);
        }
    }

    private void FixedUpdate()
    {
        OnHoldMouseButton();
    }

    private void OnEnable()
    {
        _inputMap.Enable();
    }

    private void OnDisable()
    {
        _inputMap.Disable();
    }
}
