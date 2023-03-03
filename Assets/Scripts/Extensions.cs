using UnityEngine;

public static class Extensions
{
    private static LayerMask s_layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rb, Vector2 direction)
    {
        if (rb.isKinematic)
            return false;

        float radius = 0.5f;
        float distance = 0.375f;

        var hit = Physics2D.CircleCast(rb.position, radius, direction.normalized, distance, s_layerMask);
        
        return hit.collider != null && hit.rigidbody != rb;
    }

    public static bool DotTest(this Transform t, Transform other, Vector2 testDirection)
    {
        Vector2 dir = other.position - t.position;
        return Vector2.Dot(dir.normalized, testDirection.normalized) > 0.25f;
    }
}
