using UnityEngine;

public static class VectorExtensions 
{
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
    
    public static Vector3 ToVector3(this Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }
    
    public static Vector2 Clamp(this Vector2 vector2, float magnitude)
    {
        return Vector2.ClampMagnitude(vector2, magnitude);
    }
}