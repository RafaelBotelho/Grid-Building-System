using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    #region Variables

    public static InputController instance;
    
    [Header("Input Values")]
    private Vector2 _mousePosition;
    private Vector2 _clickStartPosition;
    private Vector2 _clickReleasePosition;
    private Vector2 _moveCamera;
    private Vector2 _rotateCamera;
    private Vector2 _zoomCamera;
    private bool _click;
    private bool _rotateRight;
    private bool _rotateLeft;
    private bool _rotateCameraButton;

    #endregion

    #region Properties

    public Vector2 MousePosition => _mousePosition;
    public Vector2 MoveCamera => _moveCamera;
    public Vector2 RotateCamera => _rotateCamera;
    public Vector2 ZoomCamera => _zoomCamera;
    public bool Click => _click;
    public bool RotateRight => _rotateRight;
    public bool RotateLeft => _rotateLeft;
    public bool RotateCameraButton => _rotateCameraButton;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void OnMousePosition(InputValue value)
    {
        MousePositionInput(value.Get<Vector2>());
    }

    public void OnClick(InputValue value)
    {
        ClickInput(value.isPressed);
    }

    public void OnRotateObjectRight(InputValue value)
    {
        RotateObjectInput(true, value.isPressed);
    }
    
    public void OnRotateObjectLeft(InputValue value)
    {
        RotateObjectInput(false, value.isPressed);
    }

    public void OnMoveCamera(InputValue value)
    {
        MoveCameraInput(value.Get<Vector2>());
    }
    
    public void OnRotateCamera(InputValue value)
    {
        RotateCameraInput(value.Get<Vector2>());
    }
    
    public void OnRotateCameraButton(InputValue value)
    {
        RotateCameraButtonInput(value.isPressed);
    }
    
    public void OnZoomCamera(InputValue value)
    {
        ZoomCameraInput(value.Get<Vector2>());
    }

    public void OnUndo(InputValue value)
    {
        UndoInput();
    }
    
    #endregion

    #region Methods

    private void MousePositionInput(Vector2 newMousePosition)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _mousePosition = newMousePosition;
    }
    
    private void ClickInput(bool newPlaceObjectValue)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        switch (_click)
        {
            case false when newPlaceObjectValue:
                _clickStartPosition = _mousePosition;
                EventManager.OnClickButtonStart?.Invoke();
                break;
            case true when !newPlaceObjectValue:
                _clickReleasePosition = _mousePosition;
                EventManager.OnClickButtonReleased?.Invoke();
                break;
        }

        _click = newPlaceObjectValue;
    }

    private void RotateObjectInput(bool isRight, bool newRotateValue)
    {
        if (isRight)
        {
            switch (_rotateRight)
            {
                case false when newRotateValue:
                    EventManager.OnRotateObjectButtonPressed.Invoke(true);
                    break;
                case true when !newRotateValue:
                    EventManager.OnRotateObjectButtonReleased?.Invoke();
                    break;
            }

            _rotateRight = newRotateValue;
        }
        else
        {
            switch (_rotateLeft)
            {
                case false when newRotateValue:
                    EventManager.OnRotateObjectButtonPressed.Invoke(_rotateLeft);
                    break;
                case true when !newRotateValue:
                    EventManager.OnRotateObjectButtonReleased?.Invoke();
                    break;
            }

            _rotateLeft = newRotateValue;
        }
    }
    
    private void MoveCameraInput(Vector2 newMoveDirection)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _moveCamera = newMoveDirection;
    }
    
    private void RotateCameraInput(Vector2 newRotationDirection)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _rotateCamera = newRotationDirection;
    }

    private void RotateCameraButtonInput(bool newRotateCameraButtonValue)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _rotateCameraButton = newRotateCameraButtonValue;
    }
    
    private void ZoomCameraInput(Vector2 newZoomDirection)
    {
        _zoomCamera = newZoomDirection.normalized;
    }

    private void UndoInput()
    {
        EventManager.OnUndoActionButtonPressed?.Invoke();
    }
    
    public bool ClickInSamePosition()
    {
        return _clickStartPosition == _clickReleasePosition;
    }
    
    #endregion
}