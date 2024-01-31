using UnityEngine;

public class PreviewVisualController : MonoBehaviour
{
    #region Variables

    private Renderer _myRenderer;
    private Material _defaultMaterial;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        GetReferences();
    }

    #endregion

    #region Methods

    private void GetReferences()
    {
        _myRenderer = GetComponent<Renderer>();
        _defaultMaterial = _myRenderer.material;
    }
    
    public void SetAsInvalid(Material highLightMaterial)
    {
        if(!_myRenderer) return;
        
        _myRenderer.material = highLightMaterial;
    }

    public void SetAsValid()
    {
        if(!_myRenderer) return;
        
        _myRenderer.material = _defaultMaterial;
    }

    #endregion
}