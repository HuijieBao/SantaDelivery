using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime = 3.0f;
    float shakeCounter = 0.0f;

    public float[] curve;
    float speed;
    public float totalLength = 0;
    public int targetIndex = -1;

    float originalY;
    //Quaternion originalQ;
    float curY;
    float targetY;
    bool up = true;
    bool endShaking = false;
    void Start()
    {
        originalY = this.transform.position.y;
        //originalQ = this.transform.rotation;
        curY = originalY;

        for (int i = 0; i < curve.Length; i++)
        {
            totalLength += Mathf.Abs(curve[i]);
        }
        speed = totalLength / shakeTime;
    }


    public void StartShake()
    {
        targetIndex = -1;

        Vector3 pos = this.transform.position;
        pos.y = originalY;
        //this.transform.rotation = originalQ;
        this.transform.position = pos;

        shakeCounter = 0.0f;
        endShaking = false;
        SetNextIndexDestination();
        StartCoroutine(ShakeUpDown());
    }
    public IEnumerator ShakeUpDown()
    {
        while (shakeCounter <= shakeTime && !endShaking)
        {
            shakeCounter += Time.deltaTime;
            UpdateFloating();
            yield return null;
        }
    }

    void UpdateFloating()
    {
        curY = this.transform.position.y;

        if (up)
        {
            this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            if (curY >= targetY)
            {
                SetNextIndexDestination();
            }
            //this.transform.Rotate(this.transform.forward, 6.0f * Time.deltaTime);
        }
        else
        {
            this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            if (curY <= targetY)
            {
                SetNextIndexDestination();
            }
            //this.transform.Rotate(this.transform.forward, -6.0f * Time.deltaTime);
        }

    }

    void SetNextIndexDestination()
    {
        curY = this.transform.position.y;
        targetIndex++;
        if (targetIndex < curve.Length - 1)
        {
            targetY = curY + curve[targetIndex];
            if (curve[targetIndex] > 0)
            {
                up = true;
            }
            else
            {
                up = false;
            }
        }
        else if (targetIndex == curve.Length - 1)
        {
            targetY = originalY;
            if (curY > originalY)
            {
                up = false;
            }
            else
            {
                up = true;
            }
        }
        else
        {
            endShaking = true;
        }
    }
}

