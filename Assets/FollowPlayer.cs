using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Shake shake;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public Vector3 newPos;

    void LateUpdate()
    {
    newPos = target.position + offset;
        if (shake.start == false)
        {
            transform.position = newPos;
        }
    }
}
