using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();    
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            playerMovement.Move(Vector3.up);
        if (Input.GetKey(KeyCode.A))
            playerMovement.Move(Vector3.left);
        if (Input.GetKey(KeyCode.S))
            playerMovement.Move(Vector3.down);
        if (Input.GetKey(KeyCode.D))
            playerMovement.Move(Vector3.right);

        //mouse ray detecntion TESTE
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, mousePosition, 1f);
            Debug.Log(hit.collider.name);
        }
    }
}
