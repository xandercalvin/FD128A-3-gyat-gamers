using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollect : MonoBehaviour
{
    private int collectibleCount;

    public int winCount;
    public GameObject winScreen;

    public AudioSource collectSound;

    public float interactDistance;
    public TMP_Text interactText;
    public TMP_Text interactObjectNameText;
    public Transform camera_;

    // Start is called before the first frame update
    void Start()
    {
        collectibleCount = 0;
        winScreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interactText.text = "";
        interactObjectNameText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();
        Interact();
    }

    public void PickUpCollectible(GameObject collect)
    {
        collectibleCount++;
        if (collectSound != null)
        {
            collectSound.Play();
        }

        collect.gameObject.SetActive(false);

        if (collectibleCount >= winCount)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

    }

    public void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, interactDistance)) //here we are sending out a raycast in front of the player, which is constantly checking if it is touching objects that we can interact with, so it can prompt the player to interact. in this case, we just have an interactable door, so if the player raycast hits an object tagged as Door, it will show us the prompt that this object is interactable.
        {
            Debug.Log(hit.collider.gameObject.name);
            Debug.Log(hit.collider.tag);
            //this raycast also uses a distance we set in the inspector, which we can mess around with so make sure we can't interact with things too far away from us, as well as to make sure we can interact with things that feel close enough to be able to. 
            if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("Collect"))
            {
                if (interactText != null)
                {
                    interactText.text = "E";
                }
                if (hit.collider.CompareTag("Collect") && interactObjectNameText != null)
                {
                    interactObjectNameText.text = hit.collider.gameObject.name;
                }
            }
            else
            {
                if (interactText != null)
                {
                    interactText.text = "";
                }
                if(interactObjectNameText != null)
                {
                    interactObjectNameText.text = "";
                }  
                
            }
        }
        else
        {
            if (interactText != null)
            {
                interactText.text = "";
            }
            if (interactObjectNameText != null)
            {
                interactObjectNameText.text = "";
            }
        }
    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E)) //this method is checking for the interactable object once we press the button to interact. We again have to that the object in front of us is interactable, and if the object is tagged as Door, it gets the script that is attached to the door and tells it to execute the Interact method. In our door script, that means to close it if it is open, and open it if it is closed. 
        {
            RaycastHit hit;
            if (Physics.Raycast(camera_.transform.position, camera_.transform.forward, out hit, interactDistance))
            {
                //Debug.Log(hit.collider.name);
                //if (hit.collider.CompareTag("Door"))
                //{
                //    Debug.Log("hit door");
                //    hit.collider.gameObject.GetComponent<OpenDoor>().Interact();
                //}
                if (hit.collider.CompareTag("Collect"))
                {
                    Debug.Log("collected");
                    PickUpCollectible(hit.collider.gameObject);
                }
            }
        }
    }

}
