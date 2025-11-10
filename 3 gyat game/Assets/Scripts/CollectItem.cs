using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectItem : MonoBehaviour
{
    private int collectibleCount;
    public TMP_Text countText;

    public int winCount;
    public GameObject winScreen;

    public AudioSource collectSound;
    // Start is called before the first frame update
    void Start()
    {
        collectibleCount = 0;
        countText.text = "Collected: 0";
        winScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collect")
        {
            collectibleCount++;
            countText.text = "Collected: " + collectibleCount.ToString();
            if(collectSound != null)
            {
                collectSound.Play();
            }
            
            Destroy(other.gameObject);
            if(collectibleCount == winCount)
            {
                winScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }
    }
}
