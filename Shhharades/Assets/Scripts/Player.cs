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
    private Vector2 inputVector;

    [SerializeField] Transform canvas;

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

        body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (canvas.gameObject.activeInHierarchy == false)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        audioSource.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
        loudness = GetAverageVolume() * sensitivity;
        Debug.Log("Loudness: " + loudness);
        if (loudness > 5)
        {
            //body.AddForce(transform.forward * loudness);
            var velocity = transform.forward * (loudness / 10);
            var position = body.position + velocity * Time.fixedDeltaTime;
            body.MovePosition(position);

            Debug.Log("Velocity: " + body.velocity);
        }

        //transform.Rotate(0, 0.5f * inputVector.x, 0);
        var rotation = new Vector3(0f, inputVector.x, 0f) * 0.5f;
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        body.MoveRotation(body.rotation * deltaRotation);
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
