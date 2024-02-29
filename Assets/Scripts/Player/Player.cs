using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    Rigidbody rigidbodyPlayer;
    CapsuleCollider capsule;
    [SerializeField] Animator animator;
    [SerializeField] Camera cameraPl;
    [SerializeField] TriggerUp triggerUp;
    [SerializeField] TriggerJump triggerJump;
    bool isCtrl = false;
    Vector3 vector;
    float su;

    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsule.height = 2;
    }

    public void MovePlayer(Vector3 move, float sprint)
    {
        animator.SetFloat("Horizontal", move.x);
        animator.SetFloat("Vertical", move.z+sprint);
        var az = math.abs(move.x);
        animator.SetFloat("VelVert", move.z!=0? move.z + sprint:az);
        vector = transform.TransformDirection(move.normalized)*4;
        
        float maxspeed;
        if (isCtrl) { maxspeed = PlayerStats.me.speed * 0.5f; }
        else if (sprint !=0&& !triggerUp.isUstal) { maxspeed = PlayerStats.me.sprintSpeed* sprint; maxspeed = maxspeed < PlayerStats.me.speed ? PlayerStats.me.speed : maxspeed; }
        else { maxspeed = PlayerStats.me.speed; }
        maxspeed /= 4;
        var velosityaff = new Vector2(rigidbodyPlayer.velocity.x, rigidbodyPlayer.velocity.z).magnitude/ maxspeed / maxspeed;
        vector -= rigidbodyPlayer.velocity / maxspeed;
       
        vector.y = rigidbodyPlayer.velocity.y;
        var si = move.normalized.magnitude - velosityaff;
        si = si <= 0 ? 0 : si;
        su = si;
        if (!triggerJump.isJump && !triggerUp.isTrogat) { vector.x *= 0.1f; vector.z *= 0.1f; }
        else if (triggerUp.isTrogat) { 
            transform.position += new Vector3(0, (si)* move.z * maxspeed* PlayerStats.me.zalesSpeed * Time.deltaTime);
             }

        float summ = (sprint * PlayerStats.me.staminaSprint) + (triggerUp.isTrogat&& !triggerJump.isJump ? PlayerStats.me.staminaZalesanie : 0);
        triggerUp.StaminaUpdate(!triggerUp.isUstal ? move.magnitude* summ * maxspeed / 5:0);
        
        vector.y -= rigidbodyPlayer.velocity.y;

        rigidbodyPlayer.AddForce(vector, ForceMode.VelocityChange);
        animator.SetBool("isJump", !triggerJump.isJump);
    }

    public void AnimRead(bool iz)
    {
        animator.SetBool("isRead", iz);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (triggerUp.isTrogat&& su > 0 && !triggerJump.isJump)
        {
            var p = collision.GetContact(0);
            triggerUp.PovorotModel(p);
            
        }
        
        var vis = collision.GetContact(0).point.y - transform.position.y +1;
        vis *= 2;
        animator.SetBool("isTrogat", triggerUp.isTrogat && vis>0.1f);
        float v = animator.GetFloat("Visota");
        animator.SetFloat("Visota", v + (vis - v) * 0.3f);
        Debug.Log(vis);
    }

    private bool oneJump = false;
    public void JumpPlayer(float jump)
    {
        if (jump != 0 && !oneJump)
        {
            animator.SetInteger("Jump", 1);
            vector = rigidbodyPlayer.velocity;
            if (triggerJump.isJump&& !triggerUp.isUstal) {
                
                vector.y =vector.y*0.1f+ jump * PlayerStats.me.heithJump; 
                triggerUp.StaminaUpdate(PlayerStats.me.staminaJump);
                oneJump = true;
                
            }
            rigidbodyPlayer.velocity = vector;
            
        }
        if (!triggerJump.isJump) oneJump = false;
        animator.SetInteger("Jump", 2);
        if (triggerJump.isJump) { animator.SetInteger("Jump", 3); }
        
    }

  
    public void CTRLPlayer(bool ctrl)
    {
        Vector3 cent = capsule.center;
        Vector3 cam = cameraPl.transform.localPosition;
        animator.SetBool("isCTRL", ctrl);
        if (ctrl) { capsule.height = 1;cent.y = -0.5f; capsule.center = cent; cam.y = -0.1f; cameraPl.transform.localPosition = cam; isCtrl = true; triggerUp.ThisActive(false); triggerUp.isTrogat = false; }
        else { capsule.height = 2; cent.y = 0; capsule.center = cent; cam.y = 0.7f; cameraPl.transform.localPosition = cam; isCtrl = false; triggerUp.isTrogat = false; triggerUp.ThisActive(true); }
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
