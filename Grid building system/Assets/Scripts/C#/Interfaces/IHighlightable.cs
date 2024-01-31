using UnityEngine;

public interface IHighlightable
{
    #region Properties

    public bool CanHighlight { get; }
    public Material MyMaterial { get; }

    #endregion
    
    #region Methods

    public void Highlight(Color highLightColor)
    {
        if(!CanHighlight) return;

        MyMaterial.EnableKeyword("_EMISSION");
        MyMaterial.SetColor("_EmissionColor", highLightColor);
    }

    public void Unhighlight()
    {
        if(!MyMaterial) return;
        
        MyMaterial.DisableKeyword("_EMISSION");
    }

    #endregion
}