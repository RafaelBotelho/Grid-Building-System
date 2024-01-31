public interface IAction
{
    public IInteractable Interactable { get; set; }
    void Execute();
    void Undo();
}