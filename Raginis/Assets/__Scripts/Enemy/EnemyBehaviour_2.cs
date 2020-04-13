using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    This enemy is designed to be of waller type. It has increased health no attack.
    It is designed to impede the players movement. It will despawn after ~15 seconds.
    
*/


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour_2 : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float explosionDelay;

    GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float explosionDistance = 2;
    private float distance;
    private bool moveable = true;


    // == private methods ==
    
    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(moveable){
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            movement = direction;
        }
    }

    private void FixedUpdate(){
        if(moveable){
            distance = Vector3.Distance(transform.position, player.transform.position);

            if(distance > explosionDistance){
                moveCharacter(movement);
            }
            else{
                moveable = false;
            }
        }
        else{
            // Deletes enemy after x seconds.
            Destroy(gameObject, explosionDelay);
        }
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
