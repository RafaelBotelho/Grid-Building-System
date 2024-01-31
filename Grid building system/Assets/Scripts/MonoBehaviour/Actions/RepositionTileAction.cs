using System.Collections.Generic;
using UnityEngine;

public class RepositionTileAction : IAction
{
    #region Variables

    private Transform _tileTransform;
    private IInteractable _interactable;
    private SO_Tile.Dir _direction = SO_Tile.Dir.Down;
    private SO_Tile.Dir _lastDirection = SO_Tile.Dir.Down;
    private SO_Tile _tile;
    private GridController _gridController;
    private GridCell _startCell;
    private GridCell _previousStartCell;
    
    #endregion

    #region Properties

    public IInteractable Interactable { get => _interactable; set => _interactable = value; }

    #endregion

    #region Methods

    public RepositionTileAction(Transform tileTransform, IInteractable interactable, SO_Tile.Dir direction,
        SO_Tile.Dir lastDirection,
        SO_Tile tile, GridController gridController, GridCell startCell, GridCell previousStartCell)
    {
        _tileTransform = tileTransform;
        _interactable = interactable;
        _direction = direction;
        _lastDirection = lastDirection;
        _tile = tile;
        _gridController = gridController;
        _startCell = startCell;
        _previousStartCell = previousStartCell;
    }

    public void Execute()
    {
        var offSet = _tile.GetRotationOffSet(_direction);
        var position = _gridController.Grid.GetWorldPosition(_startCell.GridPosition.x, _startCell.GridPosition.y,
            _startCell.GridPosition.z);

        position += new Vector3(offSet.x, 0, offSet.y) * _gridController.Grid.cellSize;

        _tileTransform.position = position;
        _tileTransform.rotation = Quaternion.Euler(0, SO_Tile.GetRotation(_direction), 0);
        
        _interactable.EndReposition();
    }

    public void Undo()
    {
        _interactable.StartReposition();
        
        var offSet = _tile.GetRotationOffSet(_lastDirection);
        var position = _gridController.Grid.GetWorldPosition(_previousStartCell.GridPosition.x,
            _previousStartCell.GridPosition.y,
            _previousStartCell.GridPosition.z);

        position += new Vector3(offSet.x, 0, offSet.y) * _gridController.Grid.cellSize;

        _tileTransform.position = position;
        _tileTransform.rotation = Quaternion.Euler(0, SO_Tile.GetRotation(_lastDirection), 0);

        _interactable.EndReposition();
    }

    #endregion
}