using System.Collections.Generic;

public class TileController : Abs_ItemController
{
    #region Variables
    
    private GridCell _startCell;
    private SO_Tile.Dir _currentDir;

    #endregion

    #region Properties

    public GridCell StartGridCell => _startCell;
    public SO_Tile.Dir CurrentDir => _currentDir;

    #endregion
    
    #region Methods
    
    public void Initialize(GridCell startCell, SO_Tile.Dir currentDir)
    {
        _currentDir = currentDir;
        _startCell = startCell;
    }
    
    #endregion
}