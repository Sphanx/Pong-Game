using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D ballRB;
    public float ballSpeed;
    public bool hasRun;
    public float speedIncreasePerRound;
    public float speedIncreaseOnTouch;
    private float storeBallSpeed = 0;

    // Start is called before the first frame update
    private void Start() {
        if(!hasRun){
            if(Input.GetKeyDown(KeyCode.Space)){
            ballSpeed += speedIncreasePerRound;
            storeBallSpeed = ballSpeed;
            startGame(ballSpeed);
            hasRun = true;
            }
        }
    }
   private void Update() {
    //start round if hasRun=false
    if(!hasRun){
        if(Input.GetKeyDown(KeyCode.Space)){
            ballSpeed += speedIncreasePerRound;
            storeBallSpeed = ballSpeed;
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
            ballSpeed = storeBallSpeed;
        }else{
            ballSpeed += speedIncreaseOnTouch;
            ballRB.velocity = ballRB.velocity.normalized * ballSpeed;

        }
        //when the ball touches an object increase speed of the ball.
    }

    //starting round fuction. Assign random direction to the ball.
    void startGame(float ballSpeed){
        ballRB = this.GetComponent<Rigidbody2D>();
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        ballRB.velocity = new Vector2(x * ballSpeed, y * ballSpeed);
    }


}
