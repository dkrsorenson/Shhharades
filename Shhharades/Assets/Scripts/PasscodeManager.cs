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
    Dictionary<string, int> displayNums;
    Dictionary<int, string> objectNums;
    Dictionary<int, bool> inPasscode;

    // Start is called before the first frame update
    void Start()
    {
        passcodeLength = 6;
        passcode = GetRandomPasscode();
        userPasscode = passcode.Substring(0,1);
        passDisplay.text = userPasscode;
        displayNums = new Dictionary<string, int>();
        objectNums = new Dictionary<int, string>();
        inPasscode = new Dictionary<int, bool>();
        InitializeInPasscode(); //sets up bool values forr inPasscode dictionary
        GenerateObjectNums(); //fills dictionary with object nums
        GenerateDisplayNums(); //gets each object's number to display (i.e. page number for next object to find)
        GameObject.Find("Player").GetComponent<Raycast>().GetNums();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            RemoveNumberFromPasscode();
        }
    }

    public void AddToUserPasscode(int num)
    {
        if (userPasscode.Length<6 && !(userPasscode.Contains(num.ToString())))
        {
            userPasscode += num.ToString();
            passDisplay.text = userPasscode;
        }
    }

    //removes the last digit from the user's passcode
    public void RemoveNumberFromPasscode()
    {
        if(userPasscode.Length>1) //remove one digit
        {
            userPasscode = userPasscode.Substring(0, userPasscode.Length - 1);
            passDisplay.text = userPasscode;
        }
        //otherwise do nothing
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

    public int GetNextNum(int index)
    {
        int passNum;
        int.TryParse(passcode.Substring(index, 1), out passNum);
        return passNum;
    }

    public Dictionary<string, int> GetDisplayNums()
    {
        return displayNums;
    }

    private void GenerateObjectNums()
    {
        objectNums.Add(0, "Tree");
        objectNums.Add(1, "Couch");
        objectNums.Add(2, "Mirror");
        objectNums.Add(3, "CandyCane");
        objectNums.Add(4, "Fireplace");
        objectNums.Add(5, "TV");
        objectNums.Add(6, "Table");
        objectNums.Add(7, "Wreath");
        objectNums.Add(8, "Lamp");
        objectNums.Add(9, "Clock");
    }

    private void GenerateDisplayNums()
    {
        int nextNum;
        int currentNum;
        string usedNums = "";

        //add proper number if in passcode
        for(int i = 1; i<passcodeLength; i++)
        {
            int.TryParse(passcode.Substring(i, 1), out nextNum);
            int.TryParse(passcode.Substring(i-1, 1), out currentNum);

            displayNums.Add(objectNums[currentNum], nextNum);

            inPasscode[currentNum] = true;
            usedNums += nextNum;
        }

        //generate random numbers for rest of objects
        for(int i = 0; i< inPasscode.Count; i++)
        {
            if(!inPasscode[i])
            {
                int num;
                do
                {
                    num = Random.Range(0, 10);
                } while(usedNums.Contains(num.ToString()));
                displayNums.Add(objectNums[i], num);
                usedNums += num;
            }
        }
    }

    private void InitializeInPasscode()
    {
        inPasscode.Add(0, false);
        inPasscode.Add(1, false);
        inPasscode.Add(2, false);
        inPasscode.Add(3, false);
        inPasscode.Add(4, false);
        inPasscode.Add(5, false);
        inPasscode.Add(6, false);
        inPasscode.Add(7, false);
        inPasscode.Add(8, false);
        inPasscode.Add(9, false);
    }
}
