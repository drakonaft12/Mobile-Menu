using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerUp : MonoBehaviour
{
    public bool isTrogat = false, isUstal = false;
    [SerializeField] TextMeshProUGUI text;
    private float maxStamina = 100, stamina = 100;
    public void StaminaUpdate(float _stamina)
    {
        if (stamina <= 0) { isUstal = true; stamina = 0; StartCoroutine(StaminaRegnerator()); }
        if(stamina >= maxStamina) { stamina = maxStamina; isUstal = false; }
        if (_stamina != 0)
        {
            stamina -= _stamina;
            StopAllCoroutines();
        }
        else
        {
            StartCoroutine(StaminaRegnerator());
        }
        
    }

    private void Update()
    {
        text.text = ((int)stamina).ToString();
    }

    private IEnumerator StaminaRegnerator()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            if(stamina >= maxStamina) { yield break; }
            else { stamina += 0.1f; yield return new WaitForSeconds(0.2f); }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != transform.parent.gameObject && !isUstal) 
        {
            isTrogat = true;
        }
        else if (isUstal)
        {
            isTrogat = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != transform.parent.gameObject)
        {
            isTrogat = false;
        }
    }
}
