public class RemoverController : Abs_InteractorController
{
    #region MyRegion

    protected override void Update()
    {
        base.Update();

        if (InputController.Click)
            HandleClick();
    }

    #endregion
    
    #region Methods

    protected override void HandleClick()
    {
        //base.HandleClick();
        if (_currentInteractable == null) return;
        
        RemoveInteractable();
    }

    private void RemoveInteractable()
    {
        var removeAction = new RemoveAction(_currentInteractable, _currentInteractable.BuildAction);
        
        _actionsController.AddAction(removeAction);
    }
    
    #endregion
}