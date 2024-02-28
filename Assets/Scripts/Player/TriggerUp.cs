using DG.Tweening;
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
    [SerializeField] GameObject model;
    private float stamina = 100;
    private CapsuleCollider collider;


    public float Stamina { get => stamina; set => stamina = value; }

    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }
    public void StaminaUpdate(float _stamina)
    {
        if (stamina <= 0) { isUstal = true; stamina = 0; StartCoroutine(StaminaRegnerator()); }
        if(stamina >= PlayerStats.me.maxStamina) { stamina = PlayerStats.me.maxStamina; isUstal = false; StopAllCoroutines(); }
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
        slider.maxValue = PlayerStats.me.maxStamina;
    }

    private IEnumerator StaminaRegnerator()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            if(stamina >= PlayerStats.me.maxStamina) { yield break; }
            else { stamina += PlayerStats.me.staminaPerTime; yield return new WaitForSeconds(0.2f); }
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

    public void PovorotModel(ContactPoint point)
    {
        var tCont = point.point;
        model.transform.LookAt(tCont);
        var r = model.transform.localRotation;
        r.x = r.z = 0;
        model.transform.localRotation = r;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != transform.parent.gameObject)
        {
            isTrogat = false;
            model.transform.rotation = new Quaternion() ;
        }
    }
}
