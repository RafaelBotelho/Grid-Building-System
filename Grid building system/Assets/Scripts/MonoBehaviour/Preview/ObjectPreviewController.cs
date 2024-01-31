using System.Collections.Generic;
using UnityEngine;

public class ObjectPreviewController : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [SerializeField] private Renderer _previewRenderer;
    [SerializeField] private MeshFilter _previewMeshFilter;
    
    private PreviewVisualController _previewVisualController;
    private List<Collider> _colliders = new List<Collider>();
    
    #endregion

    #region Properties

    public Renderer PreviewRenderer { get => _previewRenderer; set => _previewRenderer = value; }
    public MeshFilter PreviewMeshFilter { get => _previewMeshFilter; set => _previewMeshFilter = value; }
    public PreviewVisualController PreviewVisualController => _previewVisualController;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        GetReferences();
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTriggerExit(other);
    }
    
    #endregion

    #region Methods

    private void GetReferences()
    {
        _previewVisualController = GetComponentInChildren<PreviewVisualController>();
    }

    private void HandleTriggerEnter(Collider other)
    {
        _colliders.Add(other);
    }
    
    private void HandleTriggerExit(Collider other)
    {
        _colliders.Remove(other);
    }
    
    public bool IsColliding()
    {
        return _colliders.Count > 0;
    }
    
    #endregion
}