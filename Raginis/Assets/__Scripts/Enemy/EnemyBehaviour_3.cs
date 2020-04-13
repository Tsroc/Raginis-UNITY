using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This enemy is designed to be of boss type. It has increased health and an attack.
    Its attack spawns smaller enemies which chase the player.
    
*/

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour_3 : MonoBehaviour
{
    // == private fields ==
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float attackRecharge = 0.25f;
    [SerializeField] private Enemy enemy;

    private GameObject[] bosslocations;
    private GameObject bosslocation;
    private Coroutine attackCoroutine;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float distance;
    private float bossdistance = 1;
    private bool moveable = true;


    // == private methods ==

    private void Start(){
        bosslocations = GameObject.FindGameObjectsWithTag("BossLocation");
        float tempdistance;

        foreach(GameObject loc in bosslocations){
            if(!bosslocation){
                //first location should be assigned.
                bosslocation = loc;
                distance = Vector3.Distance(transform.position, loc.transform.position);
            }
            else{
                //if distance is lower, location is reassigned
                tempdistance = Vector3.Distance(transform.position, loc.transform.position);

                if(tempdistance < distance){
                    bosslocation = loc;
                    distance = tempdistance;
                    tempdistance = 0;
                }
            }
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        if(moveable){
            Vector3 direction = bosslocation.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            direction.Normalize();
            movement = direction;
        }
        else{
            if(attackCoroutine == null)
                attackCoroutine = StartCoroutine(attack());
        }
    }

    private void FixedUpdate(){
        if(moveable){
            distance = Vector3.Distance(transform.position, bosslocation.transform.position);

            if(distance > bossdistance)
                moveCharacter(movement);
            else
                moveable = false;
        }
    }

    void moveCharacter(Vector2 direction){
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    IEnumerator attack(){
        while(true){
            Instantiate(enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(attackRecharge);
        }
    }
}
