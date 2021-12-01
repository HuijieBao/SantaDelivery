using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour
{
    public XiaoFangBehavior xiaoFangBehavior;
    UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
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
            xiaoFangBehavior.EnterWin();
            uIManager.TrigWinUI();
        }
    }
}
