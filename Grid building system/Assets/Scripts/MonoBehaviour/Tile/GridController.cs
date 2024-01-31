using System;
using UnityEngine;

public class GridController : MonoBehaviour
{
    #region Variables

    [Header("Settings")]
    [SerializeField] private int _width = 1;
    [SerializeField] private int _depth = 1;
    [SerializeField] private int _height = 1;
    [SerializeField] private float _tileSize = 0;

    [Header("References")] 
    [SerializeField] private Transform _gridOrigin;
    
    private Grid<GridCell> _grid;

    #endregion

    #region Properties

    public Grid<GridCell> Grid => _grid;

    #endregion

    #region Events



    #endregion

    #region Monobehaviour

    private void Start()
    {
        InitializeGrid();
    }

    private void OnDrawGizmosSelected()
    {
        if(_grid == null) return;

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int z = 0; z < _depth; z++)
                {
                    var startPosition = _grid.GetWorldPosition(x, y, z);
                    Gizmos.DrawLine(startPosition,startPosition + Vector3.forward * _tileSize);
                    Gizmos.DrawLine(startPosition,startPosition + Vector3.right * _tileSize);
                }
            }
        }
    }

    #endregion

    #region Methods

    private void InitializeGrid()
    {
        _grid = new Grid<GridCell>(_width, _height, _depth, _tileSize, _gridOrigin.position);
            
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int z = 0; z < _depth; z++)
                {
                    var newGridCell = new GridCell(new Vector3Int(x, y, z));
                    _grid.SetValue(x, y, z, newGridCell);
                }
            }
        }
    }

    public bool IsGridLocationValid(GridCell startCell, int width, int lenght)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < lenght; z++)
            {
                var gridCell = _grid.GetValue(startCell.GridPosition.x + x, startCell.GridPosition.y,
                    startCell.GridPosition.z + z);

                if (gridCell == null)
                    return false;
            }
        }
        
        return true;
    }

    #endregion
}