using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    BoxCollider doorCollider;

    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
    }

    public void DisableCollider()
    {
        doorCollider.enabled = false;
    }

    public void EnableCollider()
    {
        doorCollider.enabled = true;
    }
}
