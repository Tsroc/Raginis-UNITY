using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{

    // == private fields ==

    [SerializeField] private float speed = 5.0f;

    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;


    // == private methods ==

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(player){
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            movement = direction;
        }
    }

    private void FixedUpdate(){
        if(player)
            moveCharacter(movement);
    }

    private void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
}
