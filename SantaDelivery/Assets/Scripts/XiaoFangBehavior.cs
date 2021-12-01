using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XiaoFangBehavior : MonoBehaviour
{
    enum AIState { Idle, StandUp, Flee, LayDown, BeCaptured };
    [SerializeField]
    AIState curState = AIState.Idle;
    Animator animator;

    public GameObject leg;
    public bool goFleeNow = false;

    public GameObject xiaoFangDestinationsGroup;
    [HideInInspector]
    public List<Transform> xiaoFangDestinations;
    [HideInInspector]
    public Transform targetFleePoint;
    public float moveSpeed = 2.0f;

    public GameObject player;
    public float awareRange = 3.0f;

    public GameObject pointLight;
    public GameObject spotLight;
    public float pointLightFlashMin = 0.1f;
    public float pointLightFlashMax = 1.0f;
    public float pointLightRandomTime = 0.0f;
    float pointLightRandomTimeCounter;

    public bool playerWin = false;

    public int fleeTimes = 0;
    public UIManager uIManager;
    void Start()
    {
        animator = this.GetComponent<Animator>();

        for (int i = 0; i < xiaoFangDestinationsGroup.transform.childCount; i++)
        {
            xiaoFangDestinations.Add(xiaoFangDestinationsGroup.transform.GetChild(i));
        }

        pointLightRandomTime = Random.Range(pointLightFlashMin, pointLightFlashMax);
    }

    public void UpdateRandomLight()
    {
        if (pointLightRandomTimeCounter < pointLightRandomTime)
        {
            pointLightRandomTimeCounter += Time.deltaTime;
        }
        else
        {
            pointLightRandomTime = Random.Range(pointLightFlashMin, pointLightFlashMax);
            pointLightRandomTimeCounter = 0.0f;
            pointLight.SetActive(!pointLight.activeSelf);
            spotLight.SetActive(!spotLight.activeSelf);
        }
    }
    public void StartFlee()
    {
        goFleeNow = true;
    }

    void GetRandomDest()
    {
        int max = xiaoFangDestinationsGroup.transform.childCount;
        int index = Random.Range(0, max);
        targetFleePoint = xiaoFangDestinations[index];
    }

    void CheckAIStateChange()
    {
        //if ((this.transform.position - player.transform.position).magnitude < awareRange)
        //{
        //    goFleeNow = true;
        //}


        if (curState == AIState.Idle)
        {
            if (goFleeNow)
            {
                EnterStandUpState();
            }
        }

        if (curState == AIState.StandUp)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                EnterFleeState();
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("LayDown"))
            {
                EnterLayDownState();
            }
        }

        if (curState == AIState.Flee)
        {
            if (CloseEnough(this.transform, targetFleePoint))
            {
                EnterLayDownState();
            }
        }

        if (curState == AIState.LayDown)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                EnterIdleState();
            }
        }

    }

    void UpdateAIState()
    {
        switch (curState)
        {
            case AIState.Idle:
                UpdateIdleState();
                break;
            case AIState.Flee:
                UpdateFleeState();
                break;
            case AIState.BeCaptured:
                UpdateBeCapturedState();
                break;
            case AIState.StandUp:
                UpdateStandUpState();
                break;
            case AIState.LayDown:
                UpdateLayDownState();
                break;
        }
    }

    public void EnterWin()
    {
        playerWin = true;
        player.GetComponent<PlayerController>().bearForWin.SetActive(false);
        pointLight.SetActive(true);
        spotLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerWin)
        {
            //AI Logic
            CheckAIStateChange();
            UpdateAIState();

            //Appeal
            UpdateRandomLight();
        }

    }


    void ExitIdleState() { }
    void EnterIdleState()
    {
        curState = AIState.Idle;
        leg.SetActive(false);
    }
    void UpdateIdleState()
    {

    }


    void ExitFleeState() { }
    void EnterFleeState()
    {
        curState = AIState.Flee;
        leg.SetActive(true);
        animator.SetBool("InRunning", true);
        GetRandomDest();

        fleeTimes++;
        if (fleeTimes == 1)
        {
            uIManager.TrigFlee1();
        }
        if (fleeTimes == 2)
        {
            uIManager.TrigFlee2();
        }
        if (fleeTimes == 3)
        {
            uIManager.TrigFlee3();
        }
    }
    void UpdateFleeState()
    {

        if (!CloseEnough(this.transform, targetFleePoint))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                Vector3 movDir = (targetFleePoint.position - this.transform.position).normalized;
                movDir.y = 0;
                movDir = movDir.normalized;
                this.transform.position += movDir * moveSpeed * Time.deltaTime;
                this.transform.Rotate(new Vector3(0, 250.0f * Time.deltaTime, 0.0f));
            }

        }
    }


    void ExitBeCapturedState() { }
    void EnterBeCapturedState() { curState = AIState.BeCaptured; }
    void UpdateBeCapturedState()
    {

    }



    void ExitStandUpState() { }
    void EnterStandUpState()
    {
        curState = AIState.StandUp;
        animator.SetBool("InRunning", true);
    }
    void UpdateStandUpState()
    {

    }


    void ExitLayDownState() { }
    void EnterLayDownState()
    {
        curState = AIState.LayDown;
        this.transform.rotation = targetFleePoint.rotation;
        goFleeNow = false;
        animator.SetBool("InRunning", false);
    }
    void UpdateLayDownState()
    {

    }


    bool CloseEnough(Transform t1, Transform t2)
    {
        return (t1.position - t2.position).magnitude < 0.1f;
    }

}
