using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRB;
    public float ballSpeed;
    public bool hasRun;
    public float ballSpeedIncrease;

    // Start is called before the first frame update
    private void Start() {
        startGame(ballSpeed);
    }
   private void Update() {
    //start round if hasRun=false
    if(!hasRun){
        if(Input.GetKeyDown(KeyCode.Space)){
            ballSpeed += ballSpeedIncrease;
            startGame(ballSpeed);
            hasRun = true;
        }
    }
   }

    //reset ball position on beginning of every round.
    private void OnCollisionEnter2D(Collision2D other) {
        //when the ball touches goalpost or outofborder tagged gameobjects, reset position of the ball.
        if(other.gameObject.CompareTag("GoalPost") || other.gameObject.CompareTag("OutOfBorders")){
            ballRB.velocity = new Vector2(0,0);
            this.gameObject.transform.position = new Vector2(0,0);
            hasRun = false;
        }
        
    }

    //starting round fuction. Assign random direction to the ball.
    void startGame(float ballSpeed){
        ballRB = this.GetComponent<Rigidbody2D>();
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        ballRB.velocity = new Vector2(x * ballSpeed, y * ballSpeed);
    }


}
