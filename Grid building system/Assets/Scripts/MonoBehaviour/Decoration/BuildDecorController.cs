using UnityEngine;

public class BuildDecorController : Abs_BuildController
{
    #region Variables

    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;

    private SO_Decoration _decoration;

    #endregion

    #region Properties

    public override SO_Item Item { get => _decoration; set => _decoration = (SO_Decoration)value; }

    #endregion
    
    #region Monobehaviour
    
    private void Update()
    {
        UpdatePreviewPosition();
        UpdatePreviewRotation();
        UpdatePreviewMaterial();
    }

    #endregion

    #region Methods
    
    protected override void TryToPlaceItem()
    {
        base.TryToPlaceItem();
        
        if (!IsLocationValid()) return;

        PlaceItem(Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers));
    }

    private void PlaceItem(Vector3 position)
    {
        var buildDecorationAction = new BuildDecorationAction(_decoration, position, _previewObject.transform.rotation);

        _actionsController.AddAction(buildDecorationAction);
    }

    #region Preview

    protected override void UpdatePreviewPosition()
    {
        _previewObject.transform.position =
            Utils.GetMouseWorldPosition(InputController.MousePosition, _buildCamera, _validLayers);
    }

    protected override void UpdatePreviewRotation()
    {
        var rotationDir = 0f;

        if (InputController.RotateRight)
            rotationDir = 1;
        if (InputController.RotateLeft)
            rotationDir = -1;

        _previewObject.transform.eulerAngles += new Vector3(0, (_rotationSpeed * rotationDir) * Time.deltaTime, 0);
    }

    protected override void UpdatePreviewMaterial()
    {
        if (IsLocationValid())
            _previewObject.PreviewVisualController.SetAsValid();
        else
            _previewObject.PreviewVisualController.SetAsInvalid(_invalidBuildPositionMaterial);
    }

    #endregion
    
    #endregion
}