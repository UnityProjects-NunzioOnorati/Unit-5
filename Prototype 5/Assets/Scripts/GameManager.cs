using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate = 1f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public AudioSource audioSource;
    public Scrollbar slider;
    public Button gameOverButton;
    public float score;
    public float spawnY = 0;
    public bool isGameActive = true;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isPaused = false;
    public float pauseCooldown = 0.5f;
    public int lives;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && isGameActive && pauseCooldown<=0)
        {
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
            pauseCooldown = 0.5f;
            Time.timeScale = isPaused? 0f: 1f;
        }
        pauseCooldown -= Time.unscaledDeltaTime;
    }

    IEnumerator SpawnTarget(){
        while(isGameActive){
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score: "+score;
    }

    public void UpdateLives()
    {
        if(lives>0)
        {
            lives--;
            livesText.text = "Lives: "+lives;
        }
        
        if(lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        gameOverButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty){
        spawnRate/=difficulty;
        titleScreen.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        livesText.text = "Lives: "+lives;
        isGameActive=true;
        score=0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        lives=3;
    }

}
