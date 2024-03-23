using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using DG.Tweening;
using static UnityEngine.AudioSettings;

[RequireComponent(typeof(PlayerBase))]
public class PlayerController : MonoBehaviour, IRotateFromTotch
{
    private const string JumpButton = "Jump";
    private const string HorisontalButtons = "Horizontal";
    private const string VerticalButtons = "Vertical";
    private const string OsX = "Mouse X";
    private const string OsY = "Mouse Y";
    [SerializeField] Canvas menu;
    [SerializeField] ModelMobileTransphorm mobile;
    [SerializeField] FixedJoystick joystick;
    Player player;
    bool isMenu = false;
    float sprint;
#if PLATFORM_ANDROID
    Vector2 Rotate;
    [SerializeField] ButtonBase sprintButton, jumpButton, menuButton;
#endif
    private void Awake()
    {
        player = GetComponent<Player>();
        menu.gameObject.SetActive(false);
#if !PLATFORM_ANDROID
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    private void Update()
    {
#if PLATFORM_ANDROID
        
           menuButton.AddListener(()=> StartCoroutine(OpenCloseMenu()));
#else
        if (Input.GetKeyDown(KeyCode.Tab))
            StartCoroutine(OpenCloseMenu());
#endif
        if (!isMenu)
        {



#if PLATFORM_ANDROID
            if (jumpButton.isClick) player.JumpPlayer(1); else player.JumpPlayer(0);
            if (sprintButton.isClick) sprint = Mathf.Lerp(sprint, 1, 0.2f); else sprint = Mathf.Lerp(sprint, 0, 0.2f);
            var move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            var rotate = Rotate;
            Rotate = Vector2.zero;

#else
            var jump = Input.GetAxis(JumpButton);
            sprint = Input.GetAxis("Shift");
            var move = new Vector3(Input.GetAxis(HorisontalButtons), 0, Input.GetAxis(VerticalButtons));
            var rotate = new Vector2(Input.GetAxis(OsX), Input.GetAxis(OsY));
            player.JumpPlayer(jump);
#endif

            player.MovePlayer(move, sprint);
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
            joystick.gameObject.SetActive(false);
#if !PLATFORM_ANDROID
            UnityEngine.Cursor.lockState = CursorLockMode.Confined;
#endif
        }
        else
        {
            player.AnimRead(false);
            yield return menu.gameObject.transform.DOLocalMoveX(-1200, 1).WaitForCompletion();
            mobile.isArm = false;
            menu.gameObject.SetActive(false);
            isMenu = false;
            joystick.gameObject.SetActive(true);
#if !PLATFORM_ANDROID
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
#endif
        }
    }

    public void EventTotch()
    {
        Rotate = new Vector2(Input.GetAxis(OsX), Input.GetAxis(OsY));
    }
}
