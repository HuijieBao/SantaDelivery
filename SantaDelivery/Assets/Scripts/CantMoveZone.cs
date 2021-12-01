using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantMoveZone : MonoBehaviour
{
    float storedSpeed = 0.0f;
    public float sendBackTime = 1.0f;
    float timeCounter = 0.0f;
    Vector3 sendBackDir;
    Camera camera;

    int trigTime = 0;
    UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController pc;
        if (other.TryGetComponent<PlayerController>(out pc))
        {
            storedSpeed = pc.moveSpeed;
            pc.moveSpeed = 0.0f;
            sendBackDir = other.transform.position - this.transform.position;
            sendBackDir.y = 0;
            sendBackDir = sendBackDir.normalized;
            StartCoroutine(SendBack(other.gameObject));
            //camera.GetComponent<CameraFollow>().StartCameraShaking();
            camera.GetComponent<CameraFollow>().inShaking = true;

            if (!uIManager.triggedBlock1)
            {
                uIManager.TrigBlock1();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController pc;
        if (other.TryGetComponent<PlayerController>(out pc))
        {
            pc.moveSpeed = storedSpeed;
        }
    }

    IEnumerator SendBack(GameObject go)
    {
        while (timeCounter < sendBackTime)
        {
            timeCounter += Time.deltaTime;
            go.transform.position += sendBackDir * 0.2f * Time.deltaTime;
            yield return null;
        }
        if (timeCounter >= sendBackTime)
        {
            timeCounter = 0.0f;
        }
    }
}
