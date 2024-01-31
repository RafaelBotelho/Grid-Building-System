using System;
using System.Collections.Generic;

public static class EventManager
{
    #region Player

    #region Input

    public static readonly GameEvent OnClickButtonStart = new GameEvent();
    public static readonly GameEvent OnClickButtonReleased = new GameEvent();
    
    public static readonly GameEvent<bool> OnRotateObjectButtonPressed = new GameEvent<bool>();
    public static readonly GameEvent OnRotateObjectButtonReleased = new GameEvent();
    
    public static readonly GameEvent OnSwitchGameplayButtonPressed = new GameEvent();
    
    public static readonly GameEvent OnUndoActionButtonPressed = new GameEvent();

    public static readonly GameEvent OnPauseButtonPressed = new GameEvent();

    #endregion

    #region Grid

    public static readonly GameEvent<GridController> OnGridChange = new GameEvent<GridController>();

    #endregion

    #region UI
    
    public static readonly GameEvent<SO_Item> OnSelectItem = new GameEvent<SO_Item>();

    #endregion

    #region SwitchGameplay

    public static readonly GameEvent OnSwitchToBuildGameplay = new GameEvent();
    public static readonly GameEvent OnSwitchToCharacterGameplay = new GameEvent();

    #endregion

    #region Pause

    public static readonly GameEvent OnPause = new GameEvent();
    public static readonly GameEvent OnUnPause = new GameEvent();

    #endregion

    #region VideoSettings

    public static readonly GameEvent<int> OnSetExhibition = new GameEvent<int>();
    public static readonly GameEvent<int> OnSetResolution = new GameEvent<int>();
    public static readonly GameEvent<int> OnSetVSync = new GameEvent<int>();
    public static readonly GameEvent<int> OnSetTargetFpsVariable = new GameEvent<int>();
    public static readonly GameEvent<int> OnSetTargetFpsFixed = new GameEvent<int>();
    public static readonly GameEvent<int> OnSetQualityLevel = new GameEvent<int>();
    public static readonly GameEvent<int> OnSetAntiAliasing = new GameEvent<int>();

    #endregion

    #region ControlsSettings

    public static readonly GameEvent<string, float> OnUpdateSensibility = new GameEvent<string, float>();

    #endregion

    #region AudioSettings

    public static readonly GameEvent OnResetAudioSettings = new GameEvent();

    #endregion
    
    #endregion
}

public class GameEvent
{
    #region Event

    private event Action _action = delegate { };

    #endregion

    #region Methods

    public void Invoke()
    {
        _action?.Invoke();
    }

    public void AddListener(Action listener)
    {
        _action -= listener;
        _action += listener;
    }

    public void RemoveListener(Action listener)
    {
        _action -= listener;
    }

    #endregion
}

public class GameEvent<T>
{
    #region Event

    private event Action<T> _action = delegate { };

    #endregion

    #region Methods

    public void Invoke(T param)
    {
        _action?.Invoke(param);
    }

    public void AddListener(Action<T> listener)
    {
        _action -= listener;
        _action += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        _action -= listener;
    }

    #endregion
}

public class GameEvent<T, TU>
{
    #region Event

    private event Action<T, TU> _action = delegate { };

    #endregion

    #region Methods

    public void Invoke(T param, TU param2)
    {
        _action?.Invoke(param, param2);
    }

    public void AddListener(Action<T, TU> listener)
    {
        _action -= listener;
        _action += listener;
    }

    public void RemoveListener(Action<T, TU> listener)
    {
        _action -= listener;
    }

    #endregion
}