using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject death;
    public GameObject end;
    public GameObject pause;
    public GameObject map;
    public GameObject abilityPanel;
    public Sprite[] img;
    public GameObject myImg;
    public Text numberBottleText;
    public Text numberCoins;


    void Awake() 
    {
        PlayerStats.death=false;
    }

    void Start()
    {
        switch (Character.numberChooseHero)
        {
            case 1:
                myImg.GetComponent<Image>().sprite = img[0];
                break;
            case 2:
                myImg.GetComponent<Image>().sprite = img[1];
                break;
            case 3:
                myImg.GetComponent<Image>().sprite = img[2];
                break;
        }
        numberBottleText.text = PlayerStats.numberOfBottle.ToString();
        numberCoins.text = PlayerStats.coins.ToString();
    }

    void Update()
    {
        if (PlayerStats.HP <= 0 && !PlayerStats.death)
        {
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            map.SetActive(false);
            abilityPanel.SetActive(false);
            death.SetActive(true);
            PlayerStats.death = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                pause.SetActive(false);
                Time.timeScale = 1;
            } else if (Time.timeScale == 1) {
                pause.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (Boss.bossDeath)
        {
            Invoke("ShowEndGame", 2f);
        }
    }

    private void ShowEndGame()
    {
        map.SetActive(false);
        abilityPanel.SetActive(false);
        end.SetActive(true);
    }

    public void RestartGame()
    {
        StatsNull();
        if (GameObject.FindGameObjectWithTag("Player"))
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        StatsNull();   
        SceneManager.LoadScene("Menu");
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void ClosePanel()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateBottleNumber()
    {
        numberBottleText.text = PlayerStats.numberOfBottle.ToString();
    }

    void StatsNull()
    {
        PlayerStats.numberOfBottle = 0;
        PlayerStats.coins = 0;
    }
}
