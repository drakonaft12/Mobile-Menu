using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerJump : MonoBehaviour
{
    public bool isJump = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != transform.parent.gameObject)
        {
            isJump = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != transform.parent.gameObject)
        {
            isJump = false;
        }
    }
}
