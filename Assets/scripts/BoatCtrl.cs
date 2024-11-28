using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCtrl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float Speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DockBoat(){
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void UdockBoat(){
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
    }

    public void MoveBoat(Vector3 direction){
        Vector3 force = direction*Speed;
        rb.AddForce(force, ForceMode.Force);
    }
    public void RotateBoat(Quaternion rot){
        rb.rotation = Quaternion.Slerp(transform.rotation, rot, Time.fixedDeltaTime * 1f);
    }
}
