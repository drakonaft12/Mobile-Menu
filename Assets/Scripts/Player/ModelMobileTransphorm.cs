using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMobileTransphorm : MonoBehaviour
{
    [SerializeField] GameObject LegBone, ArmBone;
    public bool isArm { set { if (value) { transform.parent = ArmBone.transform; transform.localPosition = Vector3.zero; transform.localRotation = new Quaternion(); }
            else { transform.parent = LegBone.transform; transform.localPosition = Vector3.zero; transform.localRotation = new Quaternion(); } } }

}
