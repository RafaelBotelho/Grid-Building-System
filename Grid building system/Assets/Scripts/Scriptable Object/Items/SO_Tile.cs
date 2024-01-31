using UnityEngine;

[CreateAssetMenu (menuName = "Game/ Item/ Tile", fileName = "New Tile")]
public class SO_Tile : SO_Item
{
    #region Enums
    
    public enum Dir
    {
        Down,
        Left,
        Up,
        Right
    }

    #endregion
    
    #region Variables
    
    [SerializeField] private int _width;
    [SerializeField] private int _lenght;

    #endregion

    #region Properties
    
    public int Width => _width;
    public int Lenght => _lenght;

    #endregion

    #region Methods

    public static Dir GetNextDir(Dir currentDir)
    {
        switch (currentDir)
        {
            default:
            case Dir.Down:
                return Dir.Left;
            case Dir.Left:
                return Dir.Up;
            case Dir.Up:
                return Dir.Right;
            case Dir.Right:
                return Dir.Down;
        }
    }

    public static Dir GetPreviousDir(Dir currentDir)
    {
        switch (currentDir)
        {
            default:
            case Dir.Down:
                return Dir.Right;
            case Dir.Left:
                return Dir.Down;
            case Dir.Up:
                return Dir.Left;
            case Dir.Right:
                return Dir.Up;
        }
    }
    
    public static int GetRotation(Dir currentDir)
    {
        switch (currentDir)
        {
            default:
            case Dir.Down:
                return 0;
            case Dir.Left:
                return 90;
            case Dir.Up:
                return 180;
            case Dir.Right:
                return 270;
        }
    }
    
    public Vector2Int GetRotationOffSet(Dir currentDir)
    {
        switch (currentDir)
        {
            default:
            case Dir.Down:
                return new Vector2Int(0, 0);
            case Dir.Left:
                return new Vector2Int(0, _width);
            case Dir.Up:
                return new Vector2Int(_width, _lenght);
            case Dir.Right:
                return new Vector2Int(_lenght, 0);
        }
    }

    #endregion
}