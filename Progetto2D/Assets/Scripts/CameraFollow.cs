using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValue, maxValue;
    

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = target.position + offset;

        Vector3 boundsPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            -10f);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundsPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }


}

