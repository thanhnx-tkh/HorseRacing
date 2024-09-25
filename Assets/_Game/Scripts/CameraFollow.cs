using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    public Vector3 rotationFinish;
    public float posZFinish;

    public bool IsFinish { get; set; }
    void FixedUpdate()
    {
        if (!IsFinish)
        {
            Vector3 desiredPosition = target.position + target.rotation * locationOffset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);

            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }
        else{
            // chuyển cảnh camera khi về đính 
            transform.position = Vector3.MoveTowards(transform.position, 
                                                        new Vector3(50, 26, posZFinish),0.7f);
            Quaternion target = Quaternion.Euler(rotationFinish);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.fixedDeltaTime * 1f);
        }
    }
}
