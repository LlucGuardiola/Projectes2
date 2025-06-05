using UnityEngine;

public static class GlobalHelper 
{
    public static string GenerateUnique2d(GameObject obj)
    {
        return $"{obj.scene.name}.{obj.transform.position.x}_{obj.transform.position.y}";
    }
}
