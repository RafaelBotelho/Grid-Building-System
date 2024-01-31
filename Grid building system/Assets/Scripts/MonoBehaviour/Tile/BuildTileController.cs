using UnityEngine;

public class BuildTileController : Abs_BuildController
{
    #region Variables
    
    [SerializeField] private GridController _gridController;
    
    private SO_Tile.Dir _currentDir = SO_Tile.Dir.Down;
    private SO_Tile _tile;
    
    #endregion

    #region Properties

    public override SO_Item Item { get => _tile; set => _tile = (SO_Tile)value; }

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
    
    public override void SetItem(SO_Item newItem)
    {
        base.SetItem(newItem);
        _currentDir = SO_Tile.Dir.Down;
    }

    private void ChangeDirection(bool isRight)
    {
        _currentDir = isRight ? SO_Tile.GetNextDir(_currentDir) : SO_Tile.GetPreviousDir(_currentDir);
        UpdatePreviewRotation();
    }

    #region Build

    protected override void TryToPlaceItem()
    {
        base.TryToPlaceItem();
        
        if (!InputController.ClickInSamePosition()) return;
        
        _gridController.Grid.GetXYZ(Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers), out var x,
            out var y, out var z);
        
        var gridCell = _gridController.Grid.GetValue(x, y, z);

        if (!IsLocationValid())
            return;

        PlaceItem(gridCell);
    }
    
    protected virtual void PlaceItem(GridCell startCell)
    {
        var buildTileAction = new BuildTileAction(_currentDir, _tile, _gridController, startCell);

        _actionsController.AddAction(buildTileAction);
    }

    #endregion

    #region Preview

    protected override void UpdatePreviewRotation()
    {
        _previewObject.transform.rotation = Quaternion.Euler(0, SO_Tile.GetRotation(_currentDir), 0);
    }
    
    protected override void UpdatePreviewPosition()
    {
        if(_gridController.Grid == null) return;
        
        _gridController.Grid.GetXYZ(
            Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers), out var x,
            out var y, out var z);
        
        var offSet = _tile.GetRotationOffSet(_currentDir);
        var position = _gridController.Grid.GetWorldPosition(x, y, z);
        position += new Vector3(offSet.x, 0, offSet.y) * _gridController.Grid.cellSize;
        
        _previewObject.transform.position = position;

        if (InputController.Click)
            TryToPlaceItem();
    }

    protected override void UpdatePreviewMaterial()
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

    #endregion
}