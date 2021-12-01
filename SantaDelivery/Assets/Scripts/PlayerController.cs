using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 0.5f;

    public float ver;
    public float hor;
    public Vector3 dir;

    public float rotateSpeed = 50.0f;

    public GameObject bearForWin;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("InRunning", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("InRunning", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0.0f));
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0.0f));
        }


        //ver = Input.GetAxis("Vertical");
        //hor = Input.GetAxis("Horizontal");
        //dir = (ver * this.transform.forward + hor * this.transform.right).normalized;
        ////dir *= Time.deltaTime * moveSpeed;
        //if (ver != 0 || hor != 0)
        //{
        //    this.transform.rotation = Quaternion.LookRotation(-dir, Vector3.up);
        //    transform.Translate(this.transform.forward * Time.deltaTime * moveSpeed);
        //    animator.SetBool("InRunning", true);
        //}
        //else
        //{
        //    animator.SetBool("InRunning", false);
        //}
    }
}
