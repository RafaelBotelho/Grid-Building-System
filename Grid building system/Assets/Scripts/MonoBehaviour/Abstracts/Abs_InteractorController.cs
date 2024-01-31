using UnityEngine;

public abstract class Abs_InteractorController : MonoBehaviour
{
    #region Variables

    [SerializeField] protected Camera _buildCamera;
    
    [SerializeField] [ColorUsage(true, true)]
    protected Color _highlightColor;

    protected IInteractable _currentInteractable;
    protected Transform _currentInteractableTransform;
    private IHighlightable _currentHighlightable;
    
    protected  InputController InputController => InputController.instance;
    protected ActionsController _actionsController => ActionsController.instance;

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

    protected virtual void Update()
    {
        SearchInteractable();
        SearchHighlightable();
    }

    #endregion

    #region Methods

    protected virtual void SubscribeToEvents()
    {
        EventManager.OnClickButtonReleased.AddListener(HandleClick);
    }
    
    protected virtual void UnsubscribeToEvents()
    {
        EventManager.OnClickButtonReleased.RemoveListener(HandleClick);
    }

    #region Interact

    private void SearchInteractable()
    {
        var ray = _buildCamera.ScreenPointToRay(InputController.MousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
        
        var interactable = hit.collider.GetComponent<IInteractable>();

        if (interactable == null)
        {
            if (_currentInteractable != null)
                RemoveInteractable();
            return;
        }

        if (_currentInteractable == null)
        {
            SetInteractable(interactable, hit.collider.transform);
            return;
        }
        
        if(interactable == _currentInteractable) return;
        
        SetInteractable(interactable, hit.collider.transform);
    }

    private void SetInteractable(IInteractable interactable, Transform interactableTransform)
    {
        _currentInteractable = interactable;
        _currentInteractableTransform = interactableTransform;
    }
    
    private void RemoveInteractable()
    {
        _currentInteractable = null;
        _currentInteractableTransform = null;
    }

    protected virtual void HandleClick()
    {
        if (!InputController.ClickInSamePosition()) return;
    }
    
    #endregion

    #region Highlight

    private void SearchHighlightable()
    {
        var ray = _buildCamera.ScreenPointToRay(InputController.MousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;
        
        var highlightable = hit.collider.GetComponent<IHighlightable>();

        if (highlightable == null)
        {
            if (_currentHighlightable != null)
                UnhighlightInteractable();
            return;
        }

        if (_currentHighlightable == null)
        {
            HighlightInteractable(highlightable);
            return;
        }
        
        if(highlightable == _currentHighlightable) return;
        
        HighlightInteractable(highlightable);
    }

    private void HighlightInteractable(IHighlightable highlightable)
    {
        _currentHighlightable?.Unhighlight();

        highlightable.Highlight(_highlightColor);
        _currentHighlightable = highlightable;
    }
    
    private void UnhighlightInteractable()
    {
        _currentHighlightable.Unhighlight();
        _currentHighlightable = null;
    }

    #endregion

    #endregion
}