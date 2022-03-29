using UnityEngine;

public static class RotationHelper
{
    public static Quaternion LookAt2DWithFlip(this Transform Object, Vector3 targetPosition)
    {
        var direction = targetPosition - Object.position;
        if (Object.lossyScale.x < 0)
            direction *= -1;
        float rotateZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rotateZ);
    }
    
    public static Quaternion LookAt2D(this Vector3 objectPosition, Vector3 targetPosition)
    {
        Vector2 direction = targetPosition - objectPosition;

        return direction.GetAngle();
    }

    public static Quaternion GetAngle(this Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }

    public static Vector2 RotateVector(this Vector2 vector, float degrees)
    {
        degrees *= Mathf.Deg2Rad;
        float angleCos = Mathf.Cos(degrees);
        float angleSin = Mathf.Sin(degrees);
        return new Vector2(vector.x * angleCos - vector.y * angleSin, 
            vector.y * angleCos + vector.x * angleSin);
    }
}
