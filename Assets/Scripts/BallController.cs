using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRB;
    public float ballSpeed;
    public bool hasRun = false;

    // Start is called before the first frame update
   private void Update() {
    if(!hasRun){
        if(Input.GetKeyDown(KeyCode.Space)){
            startGame();
            hasRun = true;
        }
    }
   }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("GoalPost")){
            ballRB.velocity = new Vector2(0,0);
            this.gameObject.transform.position = new Vector2(0,0);
            hasRun = false;
        }
    }

    void startGame(){
        ballRB = this.GetComponent<Rigidbody2D>();
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        ballRB.velocity = new Vector2(x * ballSpeed, y * ballSpeed);
    }


}
