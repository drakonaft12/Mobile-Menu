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
        
        vector = transform.TransformDirection(move.normalized)*4;
        float maxspeed;
        if (isCtrl) { maxspeed = PlayerStats.me.speed * 0.5f; }
        else if (sprint&& !triggerUp.isUstal) { maxspeed = PlayerStats.me.sprintSpeed; }
        else { maxspeed = PlayerStats.me.speed; }
        maxspeed /= 4;
        var velosityaff = new Vector2(rigidbodyPlayer.velocity.x, rigidbodyPlayer.velocity.z).magnitude/ maxspeed / maxspeed;
        vector -= rigidbodyPlayer.velocity / maxspeed;
       
        vector.y = rigidbodyPlayer.velocity.y;
        if (!triggerJump.isJump && !triggerUp.isTrogat) { vector.x *= 0.1f; vector.z *= 0.1f; }
        else if (triggerUp.isTrogat) { var su = move.magnitude - velosityaff;
            transform.position += new Vector3(0, (su<=0?0: su) * maxspeed* PlayerStats.me.zalesSpeed * Time.deltaTime);
             }

        float summ = (sprint ? PlayerStats.me.staminaSprint : 0) + (triggerUp.isTrogat&& !triggerJump.isJump ? PlayerStats.me.staminaZalesanie : 0);
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
                vector.y =vector.y*0.1f+ jump * PlayerStats.me.heithJump; 
                triggerJump.isJump = false;
                triggerUp.StaminaUpdate(PlayerStats.me.staminaJump);
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
 
        transform.Rotate(Vector3.up, rotate.x* PlayerStats.me.speedX);
        var r = cameraPl.transform.localRotation;
        if (r.x>=-0.7f&& r.x<=0.7f)
        cameraPl.transform.Rotate(Vector3.left, rotate.y* PlayerStats.me.speedY);
        else {  r.x = r.x>0?0.69f:-0.69f; cameraPl.transform.localRotation = r; }
    }
}
