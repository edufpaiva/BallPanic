using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 8.0f;
    public float maxVelocity = 4.0f;

    private Rigidbody2D playerRB;

    [SerializeField]
    private Animator mAnimator;

    [SerializeField]
    private GameObject[] arrows;

    private float heigth;
    private bool canWalk;

    [SerializeField]
    private AnimationClip clip;

    [SerializeField]
    private AudioClip shootClip;

    

    // Use this for initialization
    void Start() {
        playerRB = GetComponent<Rigidbody2D>();
        heigth = -Camera.main.orthographicSize - 0.8f;
        canWalk = true;
    }

    // Update is called once per frame
    void Update() {

        ShootTheArrow();

    }

    private void FixedUpdate() {
        playerWalkKeyboard();
    }




    void playerWalkKeyboard() {
        float force = 0.0f;
        float velocity = Mathf.Abs(playerRB.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");

        if(canWalk) {
            if(h > 0) {
                if(velocity < maxVelocity) {
                    force = speed;
                }
                Vector3 scale = transform.localScale;
                scale.x = 1.0f;

                transform.localScale = scale;

                mAnimator.SetBool("Walk", true);

            } else if(h < 0) {

                if(velocity < maxVelocity) {
                    force = -speed;
                }
                Vector3 scale = transform.localScale;
                scale.x = -1.0f;

                transform.localScale = scale;
                mAnimator.SetBool("Walk", true);
            } else {
                mAnimator.SetBool("Walk", false);
            }

            playerRB.AddForce(new Vector2(force, 0));
        }


    }

    public void ShootTheArrow() {

        if(Input.GetMouseButtonDown(0)) {
            
            StartCoroutine(PlayTheShootAnimation());
            // USE ASSIM PARA CONSEGUIR MANIPULAR O GAMEOBJECT CRIADO DEPOIS DE INSTANCIADO
            //GameObject arrow1 = Instantiate(arrows[0], new Vector3(transform.position.x, heigth, 0), Quaternion.identity) as GameObject;
            Instantiate(arrows[0], new Vector3(transform.position.x, heigth, 0), Quaternion.identity);
            
        }
    }

    IEnumerator PlayTheShootAnimation (){
        canWalk = false;
        mAnimator.Play("Shoot");
        AudioSource.PlayClipAtPoint(shootClip, transform.position);
        yield return new WaitForSeconds(clip.length);
        mAnimator.SetBool("Shoot", false);
        canWalk = true;
    }
}
