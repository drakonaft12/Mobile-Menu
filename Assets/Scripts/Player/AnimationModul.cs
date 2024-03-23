using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AnimationModul : MonoBehaviour
{
    [SerializeField] Animator animator;
    private const string VerticalVelosity = "VelVert";
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Jump = "isJump";
    private const string PawOver = "isTrogat";
    private const string Height = "Visota";
    private const string ReadMobile = "isRead";
    private const string Squatting = "isCTRL";
    private const string JumpStage = "Jump";
    private const string isMirror = "isMirror";

    public float Animation(Vector3 move, float sprint, float alfa,bool isTired, bool isPawOver, bool isJump, float supportHeight)
    {
      
        animator.SetFloat(Horizontal, move.x);
        animator.SetFloat(Vertical, move.z + sprint);
        animator.SetFloat(VerticalVelosity, move.z<0?-alfa: alfa);
        animator.SetBool(isMirror, move.z < 0);

        animator.SetBool(Jump, !isJump);

        animator.SetBool(PawOver, isPawOver && supportHeight > 0.1f);

        var v = animator.GetFloat(Height);
        if (isTired) v = 0;
        animator.SetFloat(Height, v + (supportHeight - v) * 0.4f);
        if (!isPawOver) animator.SetFloat(Height, 0);

        return v;
    }

    public bool isReadMobile { set => animator.SetBool(ReadMobile, value); }
    public bool isSquatting { set => animator.SetBool(Squatting, value); }

    public int jumpStage { set => animator.SetInteger(JumpStage, value); }
}
