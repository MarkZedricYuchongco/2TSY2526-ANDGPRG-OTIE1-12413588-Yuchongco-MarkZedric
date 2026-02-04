using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;
    private int nextThreshold = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        UpdateUI();

        if (score >= nextThreshold)
        {
            ReachedThreshold();
        }
    }

    void ReachedThreshold()
    {
        Debug.Log("Threshold reached: " + nextThreshold);
        nextThreshold += 100;
        SFXManager.instance.PlaySound("LevelUp");
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
