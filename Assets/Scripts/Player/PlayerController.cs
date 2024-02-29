using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using DG.Tweening;
using static UnityEngine.AudioSettings;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private const string JumpButton = "Jump";
    private const string HorisontalButtons = "Horizontal";
    private const string VerticalButtons = "Vertical";
    private const string OsX = "Mouse X";
    private const string OsY = "Mouse Y";
    [SerializeField] Canvas menu;
    [SerializeField] ModelMobileTransphorm mobile;
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

        if (!isMenu)
        {
            var jump = Input.GetAxis(JumpButton);
            player.JumpPlayer(jump);

            var move = new Vector3(Input.GetAxis(HorisontalButtons), 0, Input.GetAxis(VerticalButtons));
            var rotate = new Vector2(Input.GetAxis(OsX), Input.GetAxis(OsY));

            player.MovePlayer(move, Input.GetAxis("Shift"));
            player.RotatePlayer(rotate);
            player.CTRLPlayer(Input.GetKey(KeyCode.LeftControl));
        }

    }
    private void FixedUpdate()
    {
        
    }

    private IEnumerator OpenCloseMenu()
    {
        if (!isMenu)
        {
            menu.gameObject.SetActive(true);
            player.AnimRead(true);
            mobile.isArm = true;
            yield return menu.gameObject.transform.DOLocalMoveX(-600, 1);
            isMenu = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            player.AnimRead(false);
            yield return menu.gameObject.transform.DOLocalMoveX(-1200, 1).WaitForCompletion();
            mobile.isArm = false;
            menu.gameObject.SetActive(false);
            isMenu = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
