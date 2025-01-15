using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wiz : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float speed = 5f; // Speed at which the object chases the player
    public bool enablechase = false;

    void Update()
    {
        if(enablechase){
        // Ensure the player reference is set
        if (player != null)
        {
            // Move the GameObject towards the player
            Vector3 target = new Vector3(player.position.x, transform.position.y,player.position.z);
            Vector3 direction = (target - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Optional: If you want to make the chaser look towards the player
            // You can uncomment the following line to make it rotate towards the player.
           transform.LookAt(target);
        }
    }
    }
}