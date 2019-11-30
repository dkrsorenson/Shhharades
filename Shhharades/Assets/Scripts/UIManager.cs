using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] Transform lightFloor;
    [SerializeField] Transform lightCeiling;
    [SerializeField] Transform lightFront;
    [SerializeField] Transform lightBack;
    [SerializeField] Transform lightLeft;
    [SerializeField] Transform lightRight;
    public int totalTime;
    private int minutes;
    private int seconds;
    private bool timeBuffer;
    private string[] randomMessages;

    // Start is called before the first frame update
    void Start()
    {

        timeBuffer = false;
        totalTime = 600;
        minutes = totalTime / 60;
        seconds = totalTime % 60;

        randomMessages = new string[3];
        randomMessages[0] = "Too Loud!!";
        randomMessages[1] = "Be Quiet!!";
        randomMessages[2] = "Shhhhhhh!!";

        //timer.text = minutes.ToString() + ":" +seconds.ToString();
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {

        minutes = totalTime / 60;
        seconds = totalTime % 60;
        Mathf.Clamp(minutes, 0, 60);
        Mathf.Clamp(seconds, 0, 60);
        string minutesZero = minutes > 9 ? "" : "0";
        string secondsZero = seconds > 9 ? "" : "0";

        timer.text = minutesZero+minutes.ToString() + ":" + secondsZero+seconds.ToString();

        if(totalTime<=0)
        {
            float delayTime = 3f; //three seconds
            
            //turn lights on
            lightFloor.gameObject.SetActive(true);
            lightCeiling.gameObject.SetActive(true);
            lightFront.gameObject.SetActive(true);
            lightBack.gameObject.SetActive(true);
            lightLeft.gameObject.SetActive(true);
            lightRight.gameObject.SetActive(true);

            //transition to game over screen
            Invoke("ToGameOver", delayTime);
        }
    }

    private void ToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator StartCountdown(int countdownValue = 600)
    {
        totalTime = countdownValue;
        while (totalTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            totalTime--;
        }
    }

    public void TimeBuffer()
    {
        timeBuffer = false;
        warningText.text = "";
        warningText.color = Color.white;
        timer.color = Color.white;
    }

    public void TakeTimeOff(int time)
    {
        if(timeBuffer==false)
        {
            warningText.text = randomMessages[Random.Range(0,3)];
            warningText.color = Color.red;
            timer.color = Color.red;
            totalTime -= time;
            if (totalTime < 0) totalTime = 0;
            timeBuffer = true;
            Invoke("TimeBuffer", 3);
        }
        
    }
}
