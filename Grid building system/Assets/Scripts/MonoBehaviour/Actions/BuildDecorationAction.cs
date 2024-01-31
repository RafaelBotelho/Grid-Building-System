using UnityEngine;

public class BuildDecorationAction : IAction
{
    #region Variables

    private IInteractable _interactable;
    private SO_Decoration _decoration;
    private Vector3 _decorationPosition;
    private Quaternion _decorationRotation;

    #endregion
    
    #region Properties

    public IInteractable Interactable { get => _interactable; set => _interactable = value; }

    #endregion
    
    #region Methods

    public BuildDecorationAction(SO_Decoration decoration, Vector3 decorationPosition, Quaternion decorationRotation)
    {
        _decoration = decoration;
        _decorationPosition = decorationPosition;
        _decorationRotation = decorationRotation;
    }
    
    public void Execute()
    {
        var decoration = Object.Instantiate(_decoration.Prefab.gameObject, _decorationPosition, _decorationRotation);
        
        _interactable = decoration.GetComponent<IInteractable>();
        _interactable.BuildAction = this;
        _interactable.MyItem = _decoration;
    }

    public void Undo()
    {
        _interactable?.Remove();
    }

    #endregion
}