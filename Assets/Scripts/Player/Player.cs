using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    Rigidbody rigidbodyPlayer;
    CapsuleCollider capsule;
    [SerializeField] float speed = 20, prStop = 0.2f;
    [SerializeField] Camera cameraPl;
    bool isJumping = true;
    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsule.height = 2;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject != gameObject)
        {
            isJumping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != gameObject)
        {
            isJumping = false;
        }
    }

    public void MovePlayer(Vector3 move, float jump)
    {
        
        Vector3 vector = transform.TransformDirection(move.normalized);
        vector *= speed/4;
        vector -= rigidbodyPlayer.velocity / speed;
        vector.y = rigidbodyPlayer.velocity.y;
        if (isJumping) { vector.y += jump*4; }
        vector.x += rigidbodyPlayer.velocity.x;
        vector.z += rigidbodyPlayer.velocity.z;
        rigidbodyPlayer.velocity = vector;
        
    }
    public void CTRLPlayer(bool ctrl)
    {
        Vector3 cent = capsule.center;
        Vector3 cam = cameraPl.transform.localPosition;
        if (ctrl) { capsule.height = 1;cent.y = -0.5f; capsule.center = cent; cam.y = -0.1f; cameraPl.transform.localPosition = cam; }
        else { capsule.height = 2; cent.y = 0; capsule.center = cent; cam.y = 0.7f; cameraPl.transform.localPosition = cam; }
    }
    public void RotatePlayer(Vector2 rotate)
    {
        transform.Rotate(Vector3.up, rotate.x);
        cameraPl.transform.Rotate(Vector3.left, rotate.y);
    }
}
