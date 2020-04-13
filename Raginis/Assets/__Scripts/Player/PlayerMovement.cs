using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//screen boundaries taken from: https://www.youtube.com/watch?v=ailbszpt_AI
public class PlayerMovement : MonoBehaviour
{

    // == private fields ==
    
    [SerializeField] private float speed = 5.0f;

    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    

    // == private methods ==

    void Start(){
        rb = GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void LateUpdate(){
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);

        // Keep the player on the screen
        Vector3 viewPos = transform.position;
        float yValue = Mathf.Clamp(viewPos.y, screenBounds.y*-1 + objectHeight, screenBounds.y - objectHeight);
        float xValue = Mathf.Clamp(viewPos.x, screenBounds.x*-1 + objectWidth, screenBounds.x - objectWidth);
        rb.position = new Vector2(xValue, yValue);
    }
}