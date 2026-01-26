using UnityEngine;

public class FollowCamera: MonoBehaviour
{
    [Header("Target")]
    public Transform target;      

    [Header("Offset")]
    public Vector3 offset;

    [Header("Smooth")]
    public float smoothSpeed;

    [Header("Boundary")]
    public bool useBound = false;
    public Vector2 minBound;
    public Vector2 maxBound;

    void LateUpdate()   
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);

        if (useBound)
        {
            smoothedPos.x = Mathf.Clamp(smoothedPos.x, minBound.x, maxBound.x);
            smoothedPos.y = Mathf.Clamp(smoothedPos.y, minBound.y, maxBound.y);
        }

        transform.position = smoothedPos;
    }
}