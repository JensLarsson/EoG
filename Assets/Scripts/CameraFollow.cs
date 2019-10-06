using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 10f;
    public Transform target;
    private Vector3 mOffset;
    public float offset = 1;

    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        mOffset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target)
        {
            Vector3 targetPosition = target.position + mOffset;
            targetPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            transform.position = targetPosition;
        }
    }
}
