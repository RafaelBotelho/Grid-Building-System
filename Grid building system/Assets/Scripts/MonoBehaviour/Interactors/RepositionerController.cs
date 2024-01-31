using UnityEngine;

public class RepositionerController : Abs_InteractorController
{
    #region Variables

    [Header("Settings")]
    [SerializeField] private float _decorRotationSpeed;
    [SerializeField] protected LayerMask _validLayers;
    
    [Header("Preview")]
    [SerializeField] protected Material _invalidBuildPositionMaterial;
    
    
    [Header("References")]
    [SerializeField] private GridController _gridController;
    
    private IInteractable _interactableSelected;
    private Transform _interactableSelectedTransform;
    private SO_Tile.Dir _currentDir = SO_Tile.Dir.Down;
    private SO_Tile _tile;
    private SO_Decoration _decoration;
    private ObjectPreviewController _previewObject;
    
    #endregion

    #region Monobehaviour
    
    protected override void Update()
    {
        base.Update();
        
        UpdatePreviewDecoration();
        UpdatePreviewTile();
    }

    #endregion

    #region Methods

    protected override void SubscribeToEvents()
    {
        base.SubscribeToEvents();
        
        EventManager.OnRotateObjectButtonPressed.AddListener(ChangeDirection);
    }

    protected override void UnsubscribeToEvents()
    {
        base.UnsubscribeToEvents();
        
        EventManager.OnRotateObjectButtonPressed.RemoveListener(ChangeDirection);
    }

    private void ChangeDirection(bool isRight)
    {
        _currentDir = isRight ? SO_Tile.GetNextDir(_currentDir) : SO_Tile.GetPreviousDir(_currentDir);
        UpdateTilePreviewRotation();
    }
    
    protected override void HandleClick()
    {
        base.HandleClick();
        
        if (_interactableSelected == null)
            SelectInteractable();
        else
            Reposition();
    }

    private void SelectInteractable()
    {
        if (_currentInteractable == null) return;
        
        _interactableSelected = _currentInteractable;
        _interactableSelectedTransform = _currentInteractableTransform;
        _interactableSelected.StartReposition();

        SetItem(_interactableSelected.MyItem);
    }

    private void Reposition()
    {
        if (_tile)
        {
            _gridController.Grid.GetXYZ(Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers), out var x,
                out var y, out var z);
            
            var tileController = _interactableSelected as TileController;
            var gridCell = _gridController.Grid.GetValue(x, y, z);
            var repositionTileAction = new RepositionTileAction(_interactableSelectedTransform, _interactableSelected,
                _currentDir, tileController.CurrentDir, _tile, _gridController, gridCell, tileController.StartGridCell);

            _actionsController.AddAction(repositionTileAction);
        }
        else
        {
            var repositionDecorAction = new RepositionDecorationAction(_interactableSelected,
                _interactableSelectedTransform, _previewObject.transform.position, _previewObject.transform.rotation,
                _interactableSelectedTransform.position, _interactableSelectedTransform.rotation);
            
            _actionsController.AddAction(repositionDecorAction);
        }
        
        _interactableSelected = null;
        _tile = null;
        _decoration = null;
    }

    private void SetItem(SO_Item newItem)
    {
        _tile = newItem as SO_Tile;
        _decoration = newItem as SO_Decoration;
        
        if (_previewObject)
            Destroy(_previewObject.gameObject);
        
        _previewObject = Instantiate(newItem.PreviewPrefab, Vector3.zero, Quaternion.identity);
    }
    
    private bool IsLocationValid()
    {
        return !_previewObject.IsColliding();
    }

    #region Tile

    private void UpdatePreviewTile()
    {
        if(!_previewObject) return;
        if(!_tile) return;
        
        UpdateTilePreviewPosition();
        UpdateTilePreviewMaterial();
    }

    private void UpdateTilePreviewPosition()
    {
        if(_gridController.Grid == null) return;
        
        _gridController.Grid.GetXYZ(
            Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers), out var x,
            out var y, out var z);
        
        var offSet = _tile.GetRotationOffSet(_currentDir);
        var position = _gridController.Grid.GetWorldPosition(x, y, z);
        position += new Vector3(offSet.x, 0, offSet.y) * _gridController.Grid.cellSize;
        
        _previewObject.transform.position = position;
    }
    
    private void UpdateTilePreviewRotation()
    {
        _previewObject.transform.rotation = Quaternion.Euler(0, SO_Tile.GetRotation(_currentDir), 0);
    }
    
    private void UpdateTilePreviewMaterial()
    {
        if(_gridController.Grid == null) return;

        UpdateMaterial();
    }
    
    private void UpdateMaterial()
    {
        if (IsLocationValid())
            _previewObject.PreviewVisualController.SetAsValid();
        else
            _previewObject.PreviewVisualController.SetAsInvalid(_invalidBuildPositionMaterial);
    }
    
    #endregion
    
    #region Decoration

    private void UpdatePreviewDecoration()
    {
        if(!_previewObject) return;
        if (!_decoration) return;
        
        UpdateDecorPreviewPosition();
        UpdateDecorPreviewRotation();
        UpdateDecorPreviewMaterial();
    }
    
    private void UpdateDecorPreviewPosition()
    {
        _previewObject.transform.position =
            Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers);
    }

    private void UpdateDecorPreviewRotation()
    {
        var rotationDir = 0f;

        if (InputController.RotateRight)
            rotationDir = 1;
        if (InputController.RotateLeft)
            rotationDir = -1;

        _previewObject.transform.eulerAngles += new Vector3(0, (_decorRotationSpeed * rotationDir) * Time.deltaTime, 0);
    }

    private void UpdateDecorPreviewMaterial()
    {
        if (IsLocationValid())
            _previewObject.PreviewVisualController.SetAsValid();
        else
            _previewObject.PreviewVisualController.SetAsInvalid(_invalidBuildPositionMaterial);
    }

    #endregion
    
    #endregion
}