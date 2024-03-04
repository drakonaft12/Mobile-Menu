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
    [SerializeField] GameObject cameraPl;
    [SerializeField] TriggerUp triggerUp;
    [SerializeField] TriggerJump triggerJump;
    bool isCtrl = false;
    Vector3 vector, vecUp;
    float su, visotaStolknoveni = 0;

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
        var normMove = move.normalized;
        animator.SetFloat("VelVert", move.z!=0? move.z + sprint:az);
        vector = transform.TransformDirection(normMove) *4;
        
        float maxspeed;
        if (isCtrl) { maxspeed = PlayerStats.me.speed /2; }
        else if (sprint !=0&& !triggerUp.isUstal) { maxspeed = PlayerStats.me.sprintSpeed* sprint; maxspeed = maxspeed < PlayerStats.me.speed ? PlayerStats.me.speed : maxspeed; }
        else { maxspeed = PlayerStats.me.speed; }
        var deltaSpeed = rigidbodyPlayer.velocity / maxspeed;
        vector -= deltaSpeed*4;
        vector /= 4;

        maxspeed /= 4;
        vector.y = 0;
        var velosityaff = new Vector2(rigidbodyPlayer.velocity.x, rigidbodyPlayer.velocity.z).magnitude / maxspeed;
        su = normMove.magnitude - velosityaff;
        su = su <= 0 ? 0 : su;
        float v = animator.GetFloat("Visota");
        if (!triggerJump.isJump && !triggerUp.isTrogat) { vector.x *= 0.1f; vector.z *= 0.1f; }
        else if (triggerUp.isTrogat) {
            var spUp = (su) * move.z * maxspeed * PlayerStats.me.zalesSpeed;
            vecUp = new Vector3(0, spUp, 0);
            if (v < 0.55f) { vector.y = vecUp.y; }
            else
            transform.position += vecUp * Time.deltaTime;
        }

        float summ = (sprint * PlayerStats.me.staminaSprint) + (triggerUp.isTrogat&& !triggerJump.isJump ? PlayerStats.me.staminaZalesanie : 0);
        triggerUp.StaminaUpdate(!triggerUp.isUstal ? move.magnitude* summ * maxspeed / 5:0);
        
  

        rigidbodyPlayer.AddForce(vector, ForceMode.VelocityChange);

        animator.SetBool("isJump", !triggerJump.isJump);

        animator.SetBool("isTrogat", triggerUp.isTrogat && visotaStolknoveni > 0.1f);
        
        if (triggerUp.isUstal) v = 0;
        animator.SetFloat("Visota", v + (visotaStolknoveni - v) * 0.4f);
    }


    public void AnimRead(bool iz)
    {
        animator.SetBool("isRead", iz);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (triggerUp.isTrogat && su > 0 && !triggerJump.isJump) 
        {
            var p = collision.GetContact(0);
            triggerUp.PovorotModel(p);
            
        }

        visotaStolknoveni = collision.GetContact(0).point.y - transform.position.y +1;
        visotaStolknoveni *= 3;
    }

    private bool oneJump = false;
    public void JumpPlayer(float jump)
    {
        animator.SetInteger("Jump", 1);
        if (jump!=0) animator.SetInteger("Jump", 2);
        if (jump != 0 && !oneJump)
        {

            
            vector = rigidbodyPlayer.velocity;
            if (triggerJump.isJump&& !triggerUp.isUstal) {
                
                vector.y =vector.y*0.1f+ jump * PlayerStats.me.heithJump; 
                triggerUp.StaminaUpdate(PlayerStats.me.staminaJump);
                oneJump = true;
            }
            rigidbodyPlayer.velocity = vector;
            
        }
        if (!triggerJump.isJump)
        {
            oneJump = false;
            
        }
        if (triggerJump.isJump && !oneJump) { animator.SetInteger("Jump", 3); }
        
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
