using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    //fields
    [SerializeField] float sensitivity;
    [SerializeField] float loudness;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] float movementSpeed = 2.0f;
    [SerializeField] float minLoudness = 2.0f;
    [SerializeField] float maxLoudness = 15.0f;
    [SerializeField] GameObject sceneManager;
    private AudioSource audioSource;
    private Rigidbody body;
    private Vector2 inputVector;
    private Slider loudnessMeter;

    [SerializeField] Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        loudnessMeter = GameObject.FindGameObjectWithTag("LoudnessMeter").GetComponent<Slider>();
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
        // Get loudness
        audioSource.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
        loudness = GetAverageVolume() * sensitivity;

        // Cap loudness at max
        if (loudness > maxLoudness) loudness = maxLoudness;
        //Debug.Log("Loudness: " + loudness);

        // Display loudness on meter
        loudnessMeter.value = loudness / maxLoudness;

        // Move character if above the minimum loudness
        if (loudness > minLoudness)
        {
            var velocity = transform.forward * (loudness / maxLoudness) * movementSpeed;
            var position = body.position + velocity * Time.fixedDeltaTime;
            //Debug.Log("Velocity: " + velocity);
            body.MovePosition(position);
        }

        if(loudness>12.0f)
        {
            sceneManager.GetComponent<UIManager>().TakeTimeOff(60);
        }

        // Rotate player
        var rotation = new Vector3(0f, inputVector.x, 0f) * rotationSpeed;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameWonTrigger")
        {
            SceneManager.LoadScene("GameWon");
        }
    }
}
