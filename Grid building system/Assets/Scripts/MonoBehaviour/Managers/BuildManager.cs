using UnityEngine;

public class BuildManager : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private BuildTileController _buildTileController;
    [SerializeField] private BuildDecorController _buildDecorController;
    
    #endregion

    #region Monobehaviour

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    #endregion

    #region Methods
    
    private void SubscribeToEvents()
    {
        EventManager.OnSelectItem.AddListener(SetBuildController);
    }

    private void UnsubscribeToEvents()
    {
        EventManager.OnSelectItem.RemoveListener(SetBuildController);
    }
    
    private void SetBuildController(SO_Item newItem)
    {
        SetTileController(newItem as SO_Tile);
        SetDecorationController(newItem as SO_Decoration);
    }

    private void SetTileController(SO_Tile newTile)
    {
        if(!newTile) return;
        
        _buildTileController.enabled = true;
        _buildDecorController.enabled = false;
        _buildTileController.SetItem(newTile);

    }

    private void SetDecorationController(SO_Decoration newDecoration)
    {
        if (!newDecoration) return;
        
        _buildDecorController.enabled = true;
        _buildTileController.enabled = false;
        _buildDecorController.SetItem(newDecoration);
    }

    #endregion
}