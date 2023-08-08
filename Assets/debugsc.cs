using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugsc : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " hit" + other.name);
    }
}
