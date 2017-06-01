using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    private float forceX, forceY;

    [SerializeField]
    private bool moveLeft, moveRight;
    [SerializeField]
    private Rigidbody2D myRigidBody;

    // Use this for initialization
    void Start() {
        setBallSpeed();
        if(Random.Range(0, 2) == 1) {
            moveRight = true;
        } else {
            moveLeft = true;
        }
    }

    // Update is called once per frame
    void Update() {
        moveBall();
    }

    void moveBall() {
        if(moveLeft) {
            Vector3 temp = transform.position;
            temp.x -= (forceX * Time.deltaTime);
            transform.position = temp;
        }

        if(moveRight) {
            Vector3 temp = transform.position;
            temp.x += (forceX * Time.deltaTime);
            transform.position = temp;
        }
    }

    void setBallSpeed() {
        forceX = 2.5f;

        switch(this.gameObject.tag) {
            case "LargestBall":
            forceY = 11.5f;
            break;

            case "LargeBall":
            forceY = 10.5f;
            break;

            case "MediumBall":
            forceY = 9.5f;
            break;

            case "SmallBall":
            forceY = 8.5f;
            break;

            case "SmallestBall":
            forceY = 7.5f;
            break;


            default:
            break;
        }

    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "BottomBrick") {
            myRigidBody.velocity = new Vector2(0, forceY);
        }

        if(col.gameObject.tag == "LeftBrick") {
            moveRight = true;
            moveLeft = false;
        }
        if(col.gameObject.tag == "RightBrick") {
            moveRight = false;
            moveLeft = true;
        }
    }



}
