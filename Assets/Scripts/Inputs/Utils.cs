using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
    
    public static Ray ScreenPointToRay(Camera camera, Vector2 position)
    {
        return camera.ScreenPointToRay(position);
    }
}
