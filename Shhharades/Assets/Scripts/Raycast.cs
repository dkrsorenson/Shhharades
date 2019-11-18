using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    private RaycastHit view;
    public float rayLength;
    private bool isNumberVisible;
    //private GameObject player;
    private bool added;
    [SerializeField] TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        rayLength = 1;
        isNumberVisible = false;
        added = false;
        //player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(player.transform.position,player.transform.forward * radius, Color.red, 0.5f);
        
        //if within range
        if(Physics.Raycast(transform.position, transform.forward, out view, rayLength))
        {
            //Debug.Log(view.collider.tag);
            switch(view.collider.tag)
            {
                case "Tree": DisplayNumber(0); break;
                case "Couch": DisplayNumber(1); break;
                case "Mirror": DisplayNumber(2); break;
                case "CandyCane": DisplayNumber(3); break;
                case "Fireplace": DisplayNumber(4); break;
                case "TV": DisplayNumber(5); break;
                case "Table": DisplayNumber(6); break;
                case "Wreath": DisplayNumber(7); break;
                case "Lamp": DisplayNumber(8); break;
                case "Clock": DisplayNumber(9); break;
            }
        }
        else
        {
            txt.text = "";
            added = false;
        }
    }

    void DisplayNumber(int num)
    {
        //display number
        txt.text = num.ToString();

        if(Input.GetKeyUp(KeyCode.E) && !added)
        {
            //add to passcode
            GameObject.Find("RiddleManager").GetComponent<RiddleManager>().addToPasscode(num);
            added = true;
        }
    }
}
