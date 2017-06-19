using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallScript : MonoBehaviour {

    private float forceX, forceY;

    [SerializeField]
    private bool moveLeft, moveRight;
    [SerializeField]
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private GameObject originalBall;

    private GameObject ball1, ball2;

    private BallScript ball1Script, ball2Script;

    [SerializeField]
    private AudioClip[] popSounds;



    // Use this for initialization
    void Awake() {
        setBallSpeed();
        InstantiateBalls();

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

    void InstantiateBalls() {
        if(this.gameObject.tag != "SmallestBall") {
            ball1 = Instantiate(originalBall);
            ball2 = Instantiate(originalBall);

            ball1Script = ball1.GetComponent<BallScript>();
            ball2Script = ball2.GetComponent<BallScript>();

            ball1.SetActive(false);
            ball2.SetActive(false);
        }
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

        if(col.gameObject.tag == "FirstArrow" || col.gameObject.tag == "SecondArrow" || col.gameObject.tag == "FirstStickyArrow" || col.gameObject.tag == "SecondStickyArrow") {
            if(gameObject.tag != "SmallestBall") {
                InitializeBallsAndTurnOffCurrentBall();
            } else {
                gameObject.SetActive(false);
            }
        }

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

    public void SetMoveLeft(bool moveLeft) {
        this.moveLeft = moveLeft;
        moveRight = !moveLeft;
    }

    public void SetMoveRight(bool moveRight) {
        this.moveRight = moveRight;
        moveLeft = !moveRight;
    }

    void InitializeBallsAndTurnOffCurrentBall() {
        Vector3 position = transform.position;

        ball1.transform.position = position;
        ball1Script.SetMoveLeft(true);

        ball2.transform.position = position;
        ball2Script.SetMoveRight(true);

        ball1.SetActive(true);
        ball2.SetActive(true);

        if(gameObject.tag != "SmallestBall") {
            if(transform.position.y > 1 && transform.position.y <= 1.3f) {
                ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
                ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 3.5f);
            } else if(transform.position.y > 1.3f) {
                ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
                ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
            } else if(transform.position.y < 1) {
                ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
                ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5.5f);
            }
        }
        

        

        AudioSource.PlayClipAtPoint(popSounds[Random.Range(0, popSounds.Length)], transform.position);
        gameObject.SetActive(false);

    }

}
