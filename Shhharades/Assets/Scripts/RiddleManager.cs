using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleManager : MonoBehaviour
{
    private string passcode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToPasscode(int num)
    {
        passcode += num.ToString();
    }
}
