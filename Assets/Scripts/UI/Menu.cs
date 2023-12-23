using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject hero;

    void Awake() 
    {
        Destroy (GameObject.FindGameObjectWithTag("Player"));
        Destroy (GameObject.FindGameObjectWithTag("Event"));
    }   

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChooseSword()
    {
        Character.numberChooseHero = 1;
    }

    public void ChooseArcher()
    {
        Character.numberChooseHero = 2;
    }

    public void ChooseWizard()
    {
        Character.numberChooseHero = 3;
    }

    public void LoadLevel()
    {
        if(Character.numberChooseHero > 0 && Character.numberChooseHero < 4)
            StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        hero.SetActive(false);
        yield return null;
        
    }

}
