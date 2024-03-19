using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TriggerPhout : MonoBehaviour
{
    [SerializeField] LPhoutStapeController stapeController;
    [SerializeField] Collider parent;
    [SerializeField] bool isLeft;
    SphereCollider trigger;
    bool isTrigg = false;
    void Start()
    {
        trigger = GetComponent<SphereCollider>();
        trigger.isTrigger = true;
        StartCoroutine(PhoutActivate());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parent != other) 
        isTrigg = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (parent != other)
            isTrigg = false;
    }

    private IEnumerator PhoutActivate()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isTrigg);
            yield return new WaitUntil(() => isTrigg);
            if (isLeft)
            {
                stapeController.LeftPhout();
            }
            else { stapeController.RingtPhout(); }
        }
    }
}
