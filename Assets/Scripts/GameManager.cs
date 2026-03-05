using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int lives = 3;
    public int score = 0;
    public int winScore = 20;

    public TextMeshProUGUI scoreText;
    public Image[] hearts;          // 3 hartjes fotos
    public Sprite fullHeart;
    public Sprite brokenHeart;
    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;
    [Header("Difficulty")]
    public EnemySpawner spawner;        // tegenstander spawner hierin zet je de eneyspawner object
    public float playerBaseSpeed = 8f;
    public PlayerController player;     //  playercontroller is dit

    public int stepScore = 3;           // elke 5 score gaat de game sneller dus moeilijker
    public float enemySpeedStep = 1.3f; // hier zet ik hoeveel sneller ze worden
    public float spawnRateStep = 0.33f; // hier zet ik ook de spawn rate sneller
    public float minSpawnRate = 0.4f;   // hier zet ik min spawn rate zodat het niet te snel word
    public float playerSpeedStep = 0.75f;// speler word ook iets sneller om te compenseren

    private int lastDifficultyLevel = 0;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        int level = score / stepScore; // het kijkt steeds hoevaak de score in step score past als het dus 5 score is dan heb je 1 level want de step score is 5

        if (level > lastDifficultyLevel)// als je level hoger is dan het vorige level dan kan je hier dus de moeilijkheid verhogen
        {
            lastDifficultyLevel = level;
            IncreaseDifficulty(level);
        }
        UpdateUI();
        if (score >= winScore)// als score gelijk of hoger is als winscore dan geef het je de win en krijg je win screen
        {
            audioSource.clip = winSound;
            audioSource.Play();

            Invoke("LoadWinScene", 1f);
        }
    }

    public void LoseLife()// hier pakt hij juist je leven af
    {
        lives--;
        UpdateUI();
        if (lives <= 0)// gelijk aan of minder dan 0 levens dan heb je dus verloren en krijg je losing screen
        {
            audioSource.clip = loseSound;
            audioSource.Play();

            Invoke("LoadLoseScene", 1f);
        }
            
    }

    void UpdateUI()// hier update hij de ui elke keer dat je score krijgt of een hartje verliest
    {
        if (scoreText != null) scoreText.text = "Score: " + score;

        // als i lagere waarde is dan hearts.length voeg dan 1 aan i toe en loop 1x er zijn 3 hartjes dus loop 4x
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < lives) ? fullHeart : brokenHeart; 
        }
    }

    void LoadWinScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScene"); // hier laden we win scherm
    }

    void LoadLoseScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverScene"); // hier laden we lose scherm
    }

    void IncreaseDifficulty(int level) // hier zetten we de game moeilijker
    {
        
        if (spawner != null)
        {

            spawner.spawnRate = Mathf.Max(minSpawnRate, spawner.spawnRate - spawnRateStep);
            spawner.RestartSpawner();

        }
        else
        {
          
        }

        Enemy.SpeedBonus = level * enemySpeedStep;
       

        if (player != null)
        {

            player.speed = playerBaseSpeed + level * playerSpeedStep;
            
        }
        else
        {
            
        }
    }


}