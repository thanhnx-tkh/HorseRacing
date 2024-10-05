using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public float posZFinish;
    public Vector3 offset;

    public Vector3 offsetFinish = new Vector3(32, 21.7f, 2000);
    public Vector3 angle = new Vector3(1.2f, -91.7f, -2f);
    public Vector3 Finish;

    public HolderHorse holderHorse;

    public bool IsFinish { get; set; }
    public bool IsSpeedDown = true;
    private void Start() {
        IsSpeedDown = true;
    }
    void FixedUpdate()
    {
        if (holderHorse.horses[0])
        {
            target = holderHorse.horses[0].transform;
            if (Vector3.Distance(target.position, Finish) <= 30f)
            {
                IsFinish = true;
                if (IsSpeedDown == true)
                {
                    for (int i = 0; i < holderHorse.horses.Count; i++)
                    {
                        holderHorse.horses[i].speed -= 20f;
                    }
                    IsSpeedDown = false;
                }

            }

        }

        if (!IsFinish)
        {
            Vector3 desiredPosition = new Vector3(0f, target.position.y, target.position.z) + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            Time.timeScale = 0.3f;
            transform.position = offsetFinish;
            transform.eulerAngles = angle;
        }
    }
}
