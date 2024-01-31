using UnityEngine;

public abstract class SO_Item : ScriptableObject
{
    #region Variables

    [Header("Settings")]
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _spriteIcon;

    [Header("References")]
    [SerializeField] protected Transform _prefab;
    [SerializeField] protected ObjectPreviewController _previewPrefab;

    #endregion

    #region Properties
    
    public string Name { get => _name; set => _name = value; }
    public Sprite SpriteIcon { get => _spriteIcon; set => _spriteIcon = value; }
    public Transform Prefab { get => _prefab; set => _prefab = value; }
    public ObjectPreviewController PreviewPrefab { get => _previewPrefab; set => _previewPrefab = value; }

    #endregion
}