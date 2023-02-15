using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public bool isPlayer1;
    private Rigidbody2D playerRB;

    // Update is called once per frame
    private void Awake() {
        playerRB = this.gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(isPlayer1){
            float verticalInput = Input.GetAxis("Vertical");
            playerRB.velocity = movementSpeed * verticalInput * Time.deltaTime * Vector2.up;
        }else{
            float verticalInput = Input.GetAxis("Vertical2");
            playerRB.velocity = movementSpeed * verticalInput * Time.deltaTime * Vector2.up;
        }

    }
}
