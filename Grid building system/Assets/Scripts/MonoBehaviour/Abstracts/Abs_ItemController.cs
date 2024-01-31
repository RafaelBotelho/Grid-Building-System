using System;
using UnityEngine;

public abstract class Abs_ItemController : MonoBehaviour, IInteractable, IHighlightable
{
    #region Variables

    [Header("Settings")]
    [SerializeField] protected bool _canInteract;
    [SerializeField] protected bool _canHighlight;

    [Header("References")]
    [SerializeField] private GameObject _visual;

    protected SO_Item _myItem;
    protected Renderer _myRenderer;
    protected MeshFilter _myMeshFilter;
    protected Material _myMaterial;
    protected Mesh _myMesh;
    protected IAction _buildAction;

    #endregion

    #region Properties

    public bool CanInteract => _canInteract;
    public bool CanHighlight => _canHighlight;
    public GameObject Visual { get => _visual; set => _visual = value; }
    public Renderer MyRenderer => _myRenderer;
    public MeshFilter MyMeshFilter => _myMeshFilter;
    public Material MyMaterial => _myMaterial;
    public Mesh MyMesh => _myMesh;
    public SO_Item MyItem { get => _myItem; set => _myItem = value; }
    public IAction BuildAction { get => _buildAction; set => _buildAction = value; }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        GetReferences();
    }
    
    #endregion

    #region Methods

    protected virtual void GetReferences()
    {
        _myRenderer = GetComponentInChildren<Renderer>();
        _myMeshFilter = GetComponentInChildren<MeshFilter>();
        _myMaterial = _myRenderer.material;
        _myMesh = _myMeshFilter.mesh;
    }
    
    public virtual void Remove()
    {
        if(!_canInteract) return;
        
        Destroy(gameObject);
    }
    
    public virtual void StartReposition()
    {
        if(!_canInteract) return;
        
        _visual.SetActive(false);
    }
    
    public virtual void EndReposition()
    {
        if(!_canInteract) return;
        
        _visual.SetActive(true);
    }
    
    #endregion
}