using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PasscodeManager : MonoBehaviour
{
    private string passcode;
    private string userPasscode;
    private int passcodeLength;
    [SerializeField] TextMeshProUGUI passDisplay;

    // Start is called before the first frame update
    void Start()
    {
        passcodeLength = 6;
        passcode = GetRandomPasscode();
        userPasscode = "";
        passDisplay.text = userPasscode;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToUserPasscode(int num)
    {
        if (userPasscode.Length<6 && !(userPasscode.Contains(num.ToString())))
        {
            userPasscode += num.ToString();
            passDisplay.text = userPasscode;
        }
        Debug.Log(userPasscode);
    }

    //generate new random passcode
    private string GetRandomPasscode()
    {
        string pass = "";
        int num;

        for (int i = 0; i < passcodeLength; i++)
        {
            do
            {
                num = Random.Range(0, 10);
            } while (pass.Contains(num.ToString()));
            pass += num.ToString();
        }

        return pass;
    }
}
