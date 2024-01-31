using System.Collections.Generic;

public class RemoveAction : IAction
{
    #region Variables

    private IInteractable _interactable;
    private IAction _undoAction;

    private List<IAction> _actionsToUpdate = new List<IAction>();

    private ActionsController _actionsController => ActionsController.instance;
    
    #endregion

    #region Properties

    public IInteractable Interactable { get => _interactable; set => _interactable = value; }

    #endregion
    
    #region Methods

    public RemoveAction(IInteractable interactable, IAction undoAction)
    {
        _interactable = interactable;
        _undoAction = undoAction;

        GetActionsToUpdate();
    }
    
    public void Execute()
    {
        _interactable?.Remove();
    }

    public void Undo()
    {
        _undoAction?.Execute();

        foreach (var action in _actionsToUpdate)
            action.Interactable = _undoAction.Interactable;
    }

    private void GetActionsToUpdate()
    {
        foreach (var action in _actionsController.Actions)
            if (action.Interactable == _interactable)
                _actionsToUpdate.Add(action);
    }
    
    #endregion
}