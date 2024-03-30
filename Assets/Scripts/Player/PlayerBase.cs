using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public abstract class PlayerBase : MonoBehaviour
{

    Rigidbody rigidbodyPlayer;
    CapsuleCollider capsule;
    [SerializeField] protected AnimationModul animator;
    [SerializeField] protected GameObject cameraPl;
    [SerializeField] TriggerUp triggerUp;
    [SerializeField] TriggerJump triggerJump;
    protected bool isCtrl = false;
    Vector3 vector, vecUp;
    float su, collisionHeight = 0, v;



    protected virtual float SpeedCharacter => PlayerStats.me.speed;
    protected virtual float SprintSpeedCharacter => PlayerStats.me.sprintSpeed;
    protected virtual float ÑlimbingSpeedCharacter => PlayerStats.me.climbingSpeed;
    protected virtual float StaminaÑlimbingCharacter => PlayerStats.me.staminaÑlimbing;
    protected virtual float StaminaSprintCharacter => PlayerStats.me.staminaSprint;
    protected virtual float HeithJumpCharacter => PlayerStats.me.heithJump;
    protected virtual float StaminaJumpCharacter => PlayerStats.me.staminaJump;
    protected virtual float SpeedAxisXCharacter => PlayerStats.me.speedX;
    protected virtual float SpeedAxisYCharacter => PlayerStats.me.speedY;


    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsule.height = 2;
    }

    public void MovePlayer(Vector3 move, float sprint)
    {
        var normMove = move.normalized;
        vector = transform.TransformDirection(normMove) * 4;

        float maxspeed;
        if (isCtrl) { maxspeed = SpeedCharacter / 2; }
        else if (sprint != 0 && !triggerUp.isTired)
        { maxspeed = SprintSpeedCharacter * sprint; maxspeed = maxspeed < SpeedCharacter ? SpeedCharacter : maxspeed; }
        else { maxspeed = move.z < 0 ? SpeedCharacter * 0.75f : SpeedCharacter; }
        var deltaSpeed = rigidbodyPlayer.velocity / maxspeed;
        vector -= deltaSpeed * 4;
        vector /= 4;

        maxspeed /= 4;
        vector.y = 0;
        var velosityaff = new Vector2(rigidbodyPlayer.velocity.x, rigidbodyPlayer.velocity.z).magnitude / maxspeed;
        su = normMove.magnitude - velosityaff;
        su = su <= 0 ? 0 : su;
        float alfa = triggerUp.isTouch ? ÑlimbingSpeedCharacter : maxspeed;
        v = animator.Animation(move, sprint, alfa, triggerUp.isTired, triggerUp.isTouch, triggerJump.isJump, collisionHeight);

        if (!triggerJump.isJump && !triggerUp.isTouch) { vector.x *= 0.3f; vector.z *= 0.3f; }
        else if (triggerUp.isTouch)
        {
            var spUp = (su) * move.z * maxspeed * ÑlimbingSpeedCharacter;
            vecUp = new Vector3(0, spUp, 0);
            if (v < 0.55f) { vector.y = vecUp.y; }
        }

        float summ = (sprint * StaminaSprintCharacter) + (triggerUp.isTouch && !triggerJump.isJump ? StaminaÑlimbingCharacter : 0);
        triggerUp.StaminaUpdate(!triggerUp.isTired ? move.magnitude * summ * maxspeed / 5 : 0);
        rigidbodyPlayer.AddForce(vector, ForceMode.VelocityChange);
    }

    private void FixedUpdate()
    {
        if (v >= 0.55f) transform.position += vecUp * Time.deltaTime;
    }



    private void OnCollisionStay(Collision collision)
    {
        if (triggerUp.isTouch && su > 0 && !triggerJump.isJump)
        {
            var p = collision.GetContact(0);
            triggerUp.RotateModel(p);

        }

        collisionHeight = collision.GetContact(0).point.y - transform.position.y + 1;
        collisionHeight *= 3;
    }

    private bool oneJump = false;
    public void JumpPlayer(float jump)
    {
        animator.jumpStage = 1;
        if (jump != 0) animator.jumpStage = 2;
        if (jump != 0 && !oneJump)
        {
            vector = rigidbodyPlayer.velocity;
            if (triggerJump.isJump && !triggerUp.isTired)
            {

                vector.y = vector.y * 0.1f + jump * HeithJumpCharacter;
                triggerUp.StaminaUpdate(StaminaJumpCharacter);
                oneJump = true;
            }
            rigidbodyPlayer.velocity = vector;

        }
        if (!triggerJump.isJump)
        {
            oneJump = false;

        }
        if (triggerJump.isJump && !oneJump) { animator.jumpStage = 3; }

    }


    public void CTRLPlayer(bool ctrl)
    {
        Vector3 cent = capsule.center;

        animator.isSquatting = ctrl;
        if (ctrl)
        {
            capsule.height = 1;
            cent.y = -0.5f;
            capsule.center = cent;
            isCtrl = true;
            triggerUp.ThisActive(false);
            triggerUp.isTouch = false;
        }
        else
        {
            capsule.height = 2;
            cent.y = 0;
            capsule.center = cent;
            isCtrl = false;
            triggerUp.isTouch = false;
            triggerUp.ThisActive(true);
        }
    }
    public virtual void RotatePlayer(Vector2 rotate)
    {

        transform.Rotate(Vector3.up, rotate.x * SpeedAxisXCharacter);
        var r = cameraPl.transform.localRotation;
        if (r.x >= -0.7f && r.x <= 0.7f)
            cameraPl.transform.Rotate(Vector3.left, rotate.y * SpeedAxisYCharacter);
        else { r.x = r.x > 0 ? 0.69f : -0.69f; cameraPl.transform.localRotation = r; }

        Vector3 cam = cameraPl.transform.localPosition;
        if (isCtrl) { cam.y = -0.1f; cameraPl.transform.localPosition = cam; }
        else { cam.y = 0.7f; cameraPl.transform.localPosition = cam; }
    }
}
