using UnityEngine;

public static class Utils
{
    #region Methods

    public static Vector3 GetMouseWorldPosition(Vector2 mousePosition, Camera camera, LayerMask layers)
    {
        var ray = camera.ScreenPointToRay(mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layers) ? hit.point : Vector3.zero;
    }

    #endregion
}