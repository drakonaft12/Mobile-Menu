using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        var rotate = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        var jump = Input.GetAxis("Jump");
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenCloseMenu();
        player.MovePlayer(move, jump);
        if(!isMenu) player.RotatePlayer(rotate);
        player.CTRLPlayer(Input.GetKey(KeyCode.LeftControl));
        
    }

    private void OpenCloseMenu()
    {
        if (!isMenu)
        {
            menu.gameObject.SetActive(true);
            isMenu = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            menu.gameObject.SetActive(false);
            isMenu = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
