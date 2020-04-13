using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponsController : MonoBehaviour
{

    // == private fields ==

    [SerializeField] private GameObject crosshairs;
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float firingRate = 0.25f;
    [SerializeField] private AudioClip shootClip;
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;

    private Coroutine firingCoroutine;
    private GameObject bulletParent;
    private AudioSource audioSource;
    private Vector3 target, difference;
    private float rotationZ;


    // == private methods ==

    private void Start(){
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();

        bulletParent = GameObject.Find("BulletParent");
        if (!bulletParent){
            bulletParent = new GameObject("BulletParent");
        }
    }

    void Update(){
        // Gets location of mouse, replaces mouse with corsshairs.
        target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        difference = target - this.transform.position;
        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 270);

        if(Input.GetMouseButtonDown(0)){
            firingCoroutine = StartCoroutine(FireCoroutine());
        }
        if(Input.GetMouseButtonUp(0)){
            StopCoroutine(firingCoroutine);
        }
        // Coroutine must be disabled if escape is pressed. Could not figure out how to di in other script.
        if(Input.GetKey(KeyCode.Escape)){
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireCoroutine(){
        while (true){

            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();

            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 270);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            audioSource.PlayOneShot(shootClip, shootVolume);
            yield return new WaitForSeconds(firingRate);
        }
}

    public void hideCrosshairs(){
        Destroy(crosshairs);
    }
}
