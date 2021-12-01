using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject instruction;
    public GameObject after20;

    public GameObject flee1;
    public GameObject flee2;
    public GameObject flee3;

    public GameObject block1;
    public bool triggedBlock1 = false;

    public GameObject winUI;
    public GameObject reStartUI;
    public GameObject nope;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UIByTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator UIByTime()
    {
        yield return new WaitForSeconds(1.5f);
        instruction.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        instruction.SetActive(false);
        yield return new WaitForSeconds(20.0f);
        after20.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        after20.SetActive(false);
    }

    IEnumerator TrigFlee1Helper()
    {
        flee1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        flee1.SetActive(false);
    }

    public void TrigFlee1()
    {
        StartCoroutine(TrigFlee1Helper());
    }

    IEnumerator TrigFlee2Helper()
    {
        flee2.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        flee2.SetActive(false);
    }

    public void TrigFlee2()
    {
        StartCoroutine(TrigFlee2Helper());
    }


    IEnumerator TrigFlee3Helper()
    {
        flee3.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        flee3.SetActive(false);
    }

    public void TrigFlee3()
    {
        StartCoroutine(TrigFlee3Helper());
    }

    IEnumerator TrigBlock1Helper()
    {
        block1.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        block1.SetActive(false);
    }

    public void TrigBlock1()
    {
        triggedBlock1 = true;
        StartCoroutine(TrigBlock1Helper());
    }

    public void TrigWinUI()
    {
        winUI.SetActive(true);
        reStartUI.SetActive(true);
    }

    public void Nope()
    {
        nope.SetActive(true);
    }
}
