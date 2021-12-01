using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;
    private Vector3 offset;

    public bool inShaking = false;
    CameraShake cameraShake;

    void Start()
    {
        cameraShake = this.GetComponent<CameraShake>();
        offset = this.transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (!inShaking)
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.position = player.position + offset;
            transform.LookAt(player.position);
        }
        else
        {
            StartCoroutine(StartCameraShakeHelper());
        }
    }

    public void StartCameraShaking()
    {
        if (!inShaking)
            StartCoroutine(StartCameraShakeHelper());
    }
    IEnumerator StartCameraShakeHelper()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
        //inShaking = true;
        cameraShake.StartShake();
        yield return new WaitForSecondsRealtime(cameraShake.shakeTime);
        inShaking = false;
    }
}
