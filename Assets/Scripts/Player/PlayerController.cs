using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using DG.Tweening;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Canvas menu;
    Player player;
    bool isMenu = false;

    private void Awake()
    {
        player = GetComponent<Player>();
        menu.gameObject.SetActive(false);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(OpenCloseMenu());
        var jump = Input.GetAxis("Jump");
        player.JumpPlayer(jump);

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        var rotate = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        player.MovePlayer(move, Input.GetKey(KeyCode.LeftShift));
        if (!isMenu) player.RotatePlayer(rotate);
        player.CTRLPlayer(Input.GetKey(KeyCode.LeftControl));

    }
    private void FixedUpdate()
    {
        
    }

    private IEnumerator OpenCloseMenu()
    {
        if (!isMenu)
        {
            menu.gameObject.SetActive(true);
            yield return menu.gameObject.transform.DOLocalMoveX(-600, 0.2f);
            isMenu = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            yield return menu.gameObject.transform.DOLocalMoveX(-1200, 0.2f);
            menu.gameObject.SetActive(false);
            isMenu = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
