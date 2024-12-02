using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BoatCtrl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float Speed;
    [SerializeField] public Transform parent;
    [SerializeField] Transform boatCage;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Vector3 velocity = rb.velocity;
        speed = velocity.magnitude;
         if (velocity.magnitude > 7f)
        {
            // Normalize the velocity and scale it to the max speed
            rb.velocity = velocity.normalized * 7f;
        }

    }
    public void DockBoat(){
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void enableCage(){
        
        boatCage.gameObject.SetActive(true);
    }
      public void disableCage(){
        boatCage.gameObject.SetActive(false);
    }


    public void UdockBoat(){
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
       rb.freezeRotation = true;
    }

    public void MoveBoat(Vector3 direction){
        Vector3 force = direction*Speed;
        
        rb.AddForce(force, ForceMode.Force);
    }
    public void RotateBoat(Quaternion rot){
        rb.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * 1f);
    }
}
