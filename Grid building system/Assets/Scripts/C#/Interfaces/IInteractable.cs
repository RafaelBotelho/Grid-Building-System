public interface IInteractable
{
    #region Properties

    public bool CanInteract { get; }
    public IAction BuildAction { get; set; }
    public SO_Item MyItem { get; set; }

    #endregion

    #region Methods

    public void Remove();
    public void StartReposition();
    public void EndReposition();

    #endregion
}