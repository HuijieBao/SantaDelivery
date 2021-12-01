using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigFlee : MonoBehaviour
{

    public XiaoFangBehavior xiaoFangBehavior;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (xiaoFangBehavior.pointLight.activeSelf)
        {
            PlayerController pc;
            if (other.TryGetComponent<PlayerController>(out pc))
            {
                xiaoFangBehavior.StartFlee();
            }
        }

    }

}
