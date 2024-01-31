using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    
    [Header("Rotation")]
    [SerializeField] private float _rotationSpeedMouse;

    [Header("Zoom")]
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private float _minZoom;
    [SerializeField] private float _maxZoom;

    [Header("References")]
    [SerializeField] private CinemachineVirtualCamera _buildVirtualCamera;
    
    private Transform _myTransform;
    private CinemachineTransposer _cinemachineTransposer;

    private Vector3 _followOffset;

    private InputController InputController => InputController.instance;
    
    #endregion

    #region Monobehaviour

    private void Awake()
    {
        GetReferences();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        ZoomCameraMovement();
        MoveCameraKeyBoard();
        MoveCameraMouse();
        RotateCamera();
    }

    #endregion

    #region Methods

    private void GetReferences()
    {
        _myTransform = transform;
        _cinemachineTransposer = _buildVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }
    
    private void Initialize()
    {
        _followOffset = _cinemachineTransposer.m_FollowOffset;
    }
    
    private void MoveCameraKeyBoard()
    {
        var moveDir = _myTransform.forward * InputController.MoveCamera.y +
                      _myTransform.right * InputController.MoveCamera.x;
        
        _myTransform.position += moveDir * (_followOffset.magnitude * Time.deltaTime);
    }

    private void MoveCameraMouse()
    {
        if(!InputController.Click) return;
        
        var moveDir = _myTransform.forward * -InputController.RotateCamera.y +
                      _myTransform.right * -InputController.RotateCamera.x;
        
        _myTransform.position += moveDir * (_followOffset.magnitude / 100  * Time.deltaTime);
    }
    
    private void RotateCamera()
    {
        if(!InputController.RotateCameraButton) return;
        
        _myTransform.eulerAngles += new Vector3(0,
            InputController.RotateCamera.x * _rotationSpeedMouse * Time.deltaTime, 0);
    }
    
    private void ZoomCameraMovement()
    {
        var zoomDir = _followOffset.normalized;

        _followOffset += zoomDir * -InputController.ZoomCamera.y;

        if (_followOffset.magnitude < _minZoom)
            _followOffset = zoomDir * _minZoom;
        
        if (_followOffset.magnitude > _maxZoom)
            _followOffset = zoomDir * _maxZoom;

        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _followOffset,
            Time.deltaTime * _zoomSpeed);
    }
    
    #endregion
}