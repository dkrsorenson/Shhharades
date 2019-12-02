using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    DoorState doorState = new DoorState();      //To check the current state of the door

    /// <summary>
    /// Initial State of every variables
    /// </summary>
    private void Start()
    {
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInZone)
        {
            doorOpened = !doorOpened;           //The toggle function of door to open/close
            var isPasscodeCorrect = GameObject.Find("PasscodeManager").GetComponent<PasscodeManager>().IsPasscodeCorrect();

            // Open the door if the passcode is correct, play the jammed door animation if it is not
            if (isPasscodeCorrect)
            {
                if (doorState == DoorState.Closed && !doorAnim.isPlaying)
                {
                    doorAnim.Play("Door_Open");
                    doorState = DoorState.Opened;
                    GameObject.Find("Aparment_Door").GetComponent<DoorCollider>().DisableCollider();
                }

                if (doorState == DoorState.Opened && !doorAnim.isPlaying)
                {
                    doorAnim.Play("Door_Close");
                    doorState = DoorState.Closed;
                    GameObject.Find("Aparment_Door").GetComponent<DoorCollider>().EnableCollider();
                }
            }
            else
            {
                if (doorState == DoorState.Jammed)
                {
                    doorAnim.Play("Door_Jam");
                    doorState = DoorState.Jammed;
                }
                else if (doorState == DoorState.Jammed && !doorAnim.isPlaying)
                {
                    doorAnim.Play("Door_Open");
                    doorState = DoorState.Opened;
                }
            }
        }
    }
}
