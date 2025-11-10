using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Your Game Manager should keep track of the state of the elements in your game
// This includes things like score and win/lose conditions
public class GameManager : MonoBehaviour
{
    // UI Elements
    public Image blackScreenImage; 
    
    // Counts
    private int collectibleCount;


    // Start is called before the first frame update
    void Start()
    {
        blackScreenImage.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // These three functions can be added to buttons in editor to run them

    public void ExitGame()
    {
        Debug.Log("Exit Game to Main Menu");
        StartCoroutine(SceneLoadTimer(0));
    }
    public void StartGame()
    {
        Debug.Log("Start Game");
        StartCoroutine(SceneLoadTimer(1));
    }

    public void CloseApplication()
    {
        Debug.Log("Close Application");
        Application.Quit();
    }

    // This is a Coroutine! It allows us to execute code over a period of time rather than all at once
    // This one fades the alpha of the blackScreenImage rather than having it pop in
    IEnumerator SceneLoadTimer(int scene)
    {
        float timer = 0f;
        float duration = 0.5f;

        while(timer < duration)
        {
            timer += Time.unscaledDeltaTime;
            float lerp = timer / duration;

            blackScreenImage.color = Color.Lerp(Color.clear, Color.black, lerp);

            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.8f);

        SceneManager.LoadScene(scene);
    }
}