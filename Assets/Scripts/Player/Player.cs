using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    Rigidbody rigidbodyPlayer;
    CapsuleCollider capsule;
    [SerializeField] float speed = 6, sprintSpeed = 14, velocityMovment = 4.4f, staminaSprint = 0.6f, staminaZlesanie = 0.4f, prStop = 0.05f, speedX = 1, speedY = 1;
    [SerializeField] Camera cameraPl;
    [SerializeField] TriggerUp triggerUp;
    [SerializeField] TriggerJump triggerJump;
    bool isCtrl = false;
    Vector3 vector;

    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsule.height = 2;
        
    }

    

    

    public void MovePlayer(Vector3 move, bool sprint)
    {
        
        vector = transform.TransformDirection(move.normalized);
        vector *= velocityMovment;
        vector -= vector * prStop;
        float maxspeed;
        if (isCtrl) { maxspeed = speed * 0.5f; }
        else if (sprint&& !triggerUp.isUstal) { maxspeed = sprintSpeed; }
        else { maxspeed = speed; }
        maxspeed /= 4;
        var velosityaff = new Vector2(rigidbodyPlayer.velocity.x, rigidbodyPlayer.velocity.z).magnitude/ maxspeed / maxspeed;
        vector -= rigidbodyPlayer.velocity / maxspeed;
        
        vector.y = rigidbodyPlayer.velocity.y;
        if (!triggerJump.isJump && !triggerUp.isTrogat) { vector.x *= 0.1f; vector.z *= 0.1f; }
        else if (triggerUp.isTrogat) {
            transform.position += new Vector3(0, (move.magnitude - velosityaff) * maxspeed * Time.fixedDeltaTime);
             }
        float summ = (sprint ? staminaSprint : 0) + (triggerUp.isTrogat&& !triggerJump.isJump ? staminaZlesanie : 0);
        triggerUp.StaminaUpdate(!triggerUp.isUstal ? move.magnitude* summ * maxspeed / 5:0);
        vector.y -= rigidbodyPlayer.velocity.y;

        rigidbodyPlayer.AddForce(vector, ForceMode.VelocityChange);
        
    }
    public void JumpPlayer(float jump)
    {
        if (jump != 0)
        {
            vector = rigidbodyPlayer.velocity;
            if (triggerJump.isJump&& !triggerUp.isUstal) { 
                vector.y =vector.y*0.1f+ jump * 5; 
                triggerJump.isJump = false;
                triggerUp.StaminaUpdate(20);
            }
            rigidbodyPlayer.velocity = vector;
        }
        
    }
    public void CTRLPlayer(bool ctrl)
    {
        Vector3 cent = capsule.center;
        Vector3 cam = cameraPl.transform.localPosition;
        if (ctrl) { capsule.height = 1;cent.y = -0.5f; capsule.center = cent; cam.y = -0.1f; cameraPl.transform.localPosition = cam; isCtrl = true; triggerUp.isTrogat = true; triggerUp.ThisActive(false); }
        else { capsule.height = 2; cent.y = 0; capsule.center = cent; cam.y = 0.7f; cameraPl.transform.localPosition = cam; isCtrl = false; triggerUp.ThisActive(true); }
    }
    public void RotatePlayer(Vector2 rotate)
    {
 
        transform.Rotate(Vector3.up, rotate.x*speedX);
        var r = cameraPl.transform.localRotation;
        if (r.x>=-0.7f&& r.x<=0.7f)
        cameraPl.transform.Rotate(Vector3.left, rotate.y*speedY);
        else {  r.x = r.x>0?0.69f:-0.69f; cameraPl.transform.localRotation = r; }
    }
}
