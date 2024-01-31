using UnityEngine;

public class RepositionDecorationAction : IAction
{
    #region Variables

    private IInteractable _interactable;
    private Transform _decorationTransform;
    private Vector3 _decorationPosition;
    private Quaternion _decorationRotation;
    private Vector3 _lastDecorationPosition;
    private Quaternion _lastDecorationRotation;

    #endregion
    
    #region Properties

    public IInteractable Interactable { get => _interactable; set => _interactable = value; }

    #endregion
    
    #region Methods

    public RepositionDecorationAction(IInteractable interactable, Transform decorationTransform, Vector3 decorationPosition, Quaternion decorationRotation,
        Vector3 lastDecorationPosition, Quaternion lastDecorationRotation)
    {
        _interactable = interactable;
        _decorationTransform = decorationTransform;
        _decorationPosition = decorationPosition;
        _decorationRotation = decorationRotation;
        _lastDecorationPosition = lastDecorationPosition;
        _lastDecorationRotation = lastDecorationRotation;
    }

    public void Execute()
    {
        _decorationTransform.position = _decorationPosition;
        _decorationTransform.rotation = _decorationRotation;
        
        _interactable.EndReposition();
    }

    public void Undo()
    {
        _decorationTransform.position = _lastDecorationPosition;
        _decorationTransform.rotation = _lastDecorationRotation;
    }

    #endregion
}