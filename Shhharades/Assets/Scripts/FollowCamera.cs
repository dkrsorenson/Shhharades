using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offsetPosition;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPos = target.transform.TransformPoint(offsetPosition);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.3f);

        transform.LookAt(target.transform);
    }
}