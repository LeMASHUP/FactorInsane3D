using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, 0);
        other.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 1;
    }
}
