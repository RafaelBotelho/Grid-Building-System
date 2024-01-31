using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionsController : MonoBehaviour
{
    #region Variables

    public static ActionsController instance;

    private List<IAction> _actions = new List<IAction>();

    #endregion

    #region Properties

    public List<IAction> Actions => _actions;

    #endregion
    
    #region Monobehaviour

    private void Awake()
    {
        SetInstance();
    }

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

    private void SetInstance()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    private void SubscribeToEvents()
    {
        EventManager.OnUndoActionButtonPressed.AddListener(TryToUndo);
    }
    
    private void UnsubscribeToEvents()
    {
        EventManager.OnUndoActionButtonPressed.RemoveListener(TryToUndo);
    }
    
    public void AddAction(IAction action)
    {
        action.Execute();
        _actions.Add(action);
    }

    private void TryToUndo()
    {
        if (_actions.Count <= 0) return;

        UndoAction();
    }
    
    private void UndoAction()
    {
        var lastAction = _actions.Last();

        lastAction.Undo();
        _actions.Remove(lastAction);
    }
    
    #endregion
}