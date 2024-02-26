using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TriggerUp : MonoBehaviour
{
    public bool isTrogat = false, isUstal = false;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Slider slider;
    private float maxStamina = 100, stamina = 100;
    private CapsuleCollider collider;
    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }
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

    public void ThisActive(bool active)
    {
        collider.enabled = active;
    }
    private void Update()
    {
        text.text = ((int)stamina).ToString();
        slider.value = (int)stamina;
    }

    private IEnumerator StaminaRegnerator()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            if(stamina >= maxStamina) { yield break; }
            else { stamina += 0.3f; yield return new WaitForSeconds(0.2f); }
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
