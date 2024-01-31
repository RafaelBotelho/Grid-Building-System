using UnityEngine;

public abstract class Abs_BuildController : MonoBehaviour
{
    #region Variables

    [Header("Settings")]
    [SerializeField] protected LayerMask _validLayers;

    [Header("References")]
    [SerializeField] protected Camera _buildCamera;

    [Header("Preview")]
    [SerializeField] protected Material _invalidBuildPositionMaterial;

    protected ObjectPreviewController _previewObject;

    protected InputController InputController => InputController.instance;
    protected ActionsController _actionsController => ActionsController.instance;

    #endregion

    #region Properties

    public abstract SO_Item Item { get; set; }

    #endregion

    #region Monobehaviour

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
        HandleDisable();
    }
    
    private void Update()
    {
        UpdatePreviewPosition();
        UpdatePreviewMaterial();
    }

    #endregion

    #region Methods

    protected virtual void SubscribeToEvents()
    {
        EventManager.OnClickButtonReleased.AddListener(TryToPlaceItem);
    }

    protected virtual void UnsubscribeToEvents()
    {
        EventManager.OnClickButtonReleased.RemoveListener(TryToPlaceItem);
    }

    protected virtual void HandleDisable()
    {
        if (_previewObject)
            Destroy(_previewObject.gameObject);
        
        Item = null;
    }
    
    public virtual void SetItem(SO_Item newItem)
    {
        Item = newItem;

        if (_previewObject)
            Destroy(_previewObject.gameObject);
        
        _previewObject = Instantiate(Item.PreviewPrefab, Vector3.zero, Quaternion.identity);
    }

    #region Build

    protected virtual void TryToPlaceItem()
    {
        if (!InputController.ClickInSamePosition()) return;
    }
    
    #endregion

    #region Preview

    protected abstract void UpdatePreviewPosition();
    protected abstract void UpdatePreviewRotation();
    protected abstract void UpdatePreviewMaterial();

    protected bool IsLocationValid()
    {
        return !_previewObject.IsColliding();
    }
    
    #endregion

    #endregion
}