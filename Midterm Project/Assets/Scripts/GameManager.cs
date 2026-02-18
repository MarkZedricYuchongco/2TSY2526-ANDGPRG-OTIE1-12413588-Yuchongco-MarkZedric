using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI progressText;
    public GameObject winScreen;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip moveSound;
    public AudioClip pushSound;
    public AudioClip goalSound;
    public AudioClip winSound;

    private bool isLevelComplete = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        UpdateHUD(0, 0);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if (isLevelComplete && Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Game is finished! No more levels left.");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isLevelComplete = false;

        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            levelText = canvas.transform.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
            progressText = canvas.transform.Find("ProgressText")?.GetComponent<TextMeshProUGUI>();

            Transform winTransform = canvas.transform.Find("WinScreen");
            if (winTransform != null)
            {
                winScreen = winTransform.gameObject;
                winScreen.SetActive(false);
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }

        CheckWin();
    }

    public void CheckWin()
    {
        var boxesOnGoals = 0;
        var boxArray = GameObject.FindGameObjectsWithTag("Box");
        var goalArray = GameObject.FindGameObjectsWithTag("Goal");

        foreach (var box in boxArray)
        {
            if (box.GetComponent<BoxController>().OnGoal)
            {
                boxesOnGoals++;
            }
        }

        UpdateHUD(boxesOnGoals, goalArray.Length);

        if (boxesOnGoals == goalArray.Length)
        {
            HandleWin();
            Debug.Log("Level Complete.");
        }
    }

    private void HandleWin()
    {
        isLevelComplete = true;
        Debug.Log("Level Complete! Press R to Restart or Enter to go to the next level.");
        PlaySFX(winSound);

        if (winScreen != null) winScreen.SetActive(true);

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<PlayerController>().enabled = false;
    }

    public void RestartLevel()
    {
        isLevelComplete = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHUD(int current, int total)
    {
        if (levelText != null)
        {
            levelText.text = $"Level: {SceneManager.GetActiveScene().name}";
        }

        if (progressText != null)
        {
            progressText.text = $"Progress: {current}/{total}";
        }
    }
}
