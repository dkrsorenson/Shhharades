using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    private RaycastHit view;
    public float rayLength;
    private bool isNumberVisible;
    [SerializeField] TextMeshProUGUI txt;
    Dictionary<string, int> nums;

    // Start is called before the first frame update
    void Start()
    {
        rayLength = 1;
        isNumberVisible = false;
    }
    
    // Update is called once per frame
    void Update()
    {        
        //if within range
        if(Physics.Raycast(transform.position, transform.forward, out view, rayLength))
        {
            switch(view.collider.tag)
            {
                case "Tree": DisplayNumber(nums["Tree"]); break;
                case "Couch": DisplayNumber(nums["Couch"]); break;
                case "Mirror": DisplayNumber(nums["Mirror"]); break;
                case "CandyCane": DisplayNumber(nums["CandyCane"]); break;
                case "Fireplace": DisplayNumber(nums["Fireplace"]); break;
                case "TV": DisplayNumber(nums["TV"]); break;
                case "Table": DisplayNumber(nums["Table"]); break;
                case "Wreath": DisplayNumber(nums["Wreath"]); break;
                case "Lamp": DisplayNumber(nums["Lamp"]); break;
                case "Clock": DisplayNumber(nums["Clock"]); break;
            }
        }
        else
        {
            txt.text = "";
        }
    }

    void DisplayNumber(int num)
    {
        //display number
        txt.text = num.ToString();

        if(Input.GetKeyUp(KeyCode.E))
        {
            //add to passcode
            GameObject.Find("PasscodeManager").GetComponent<PasscodeManager>().AddToUserPasscode(num);
        }
    }

    public void GetNums()
    {
        nums = GameObject.Find("PasscodeManager").GetComponent<PasscodeManager>().GetDisplayNums();
    }
}
