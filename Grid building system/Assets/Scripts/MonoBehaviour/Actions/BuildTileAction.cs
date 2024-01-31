using System.Collections.Generic;
using UnityEngine;

public class BuildTileAction: IAction
{
    #region Variables

    private SO_Tile.Dir _direction = SO_Tile.Dir.Down;
    private SO_Tile _tile;
    private GridController _gridController;
    private GridCell _startCell;
    private IInteractable _interactable;

    #endregion

    #region Properties

    public IInteractable Interactable { get => _interactable; set => _interactable = value; }

    #endregion
    
    #region Methods

    public BuildTileAction(SO_Tile.Dir direction, SO_Tile tile, GridController gridController, GridCell startCell)
    {
        _direction = direction;
        _tile = tile;
        _gridController = gridController;
        _startCell = startCell;
    }
    
    public void Execute()
    {
        var offSet = _tile.GetRotationOffSet(_direction);
        
        if(_startCell == null) return;
        
        var position = _gridController.Grid.GetWorldPosition(_startCell.GridPosition.x, _startCell.GridPosition.y,
            _startCell.GridPosition.z);
        
        position += new Vector3(offSet.x, 0, offSet.y) * _gridController.Grid.cellSize;
        
        var tile = Object.Instantiate(_tile.Prefab, position, Quaternion.Euler(0, SO_Tile.GetRotation(_direction), 0));
        
        _interactable = tile.GetComponent<IInteractable>();
        
        if(_interactable == null)
            _interactable = tile.GetComponentInChildren<IInteractable>();
        
        _interactable.BuildAction = this;
        _interactable.MyItem = _tile;

        switch (_direction)
        {
            case SO_Tile.Dir.Down:
            case SO_Tile.Dir.Up:
                UpdateGrid(_startCell, _tile.Width, _tile.Lenght, tile);
                break;
            case SO_Tile.Dir.Left:
            case SO_Tile.Dir.Right:
                UpdateGrid(_startCell, _tile.Lenght, _tile.Width, tile);
                break;
        }
    }

    public void Undo()
    {
        _interactable?.Remove();
    }
    
    private void UpdateGrid(GridCell startCell, int width, int lenght, Transform tile)
    {
        var tileController = tile.GetComponent<TileController>();
        
        if(tileController == null)
            tileController = tile.GetComponentInChildren<TileController>();
        
        tileController.Initialize(startCell, _direction);
    }

    #endregion
}