using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //fields
    [SerializeField] float sensitivity;
    [SerializeField] float loudness;
    private AudioSource audioSource;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;
        //audioSource.mute = true;
        //body.freezeRotation = true;
       
        while(!(Microphone.GetPosition(null)>0))
        {

        }

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void FixedUpdate()
    {
        loudness = GetAverageVolume() * sensitivity;
        if (loudness > 8)
        {
            body.AddForce(transform.forward * loudness);
        }
    }

    float GetAverageVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach(float d in data)
        {
            a += Mathf.Abs(d);
        }

        return a / 256;
    }
}
