using TMPro;
using UnityEngine;

/// <summary>
/// Class <c>UIManager</c> is GameMan's loyal sidekick. If the game's <c>core</c> variables change, it will reflect that change in the UI. 
/// /// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreTextIG;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreTextEG;

    public GameObject InGameUI;
    public GameObject EndGameUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scoreTextIG.text = "0";
    }

    public void UpdateScoreText(int score)
    {
        scoreTextIG.text = score.ToString();
    }

    public void UpdateHighScoreText(int highscore)
    {
        highScoreText.text = "Best: " + highscore.ToString();
    }
    public void UpdateCoinsText(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void UpdateScoreTextEG(int score)
    {
        scoreTextEG.text = "Score: " + score.ToString();
    }

    public void ShowEndGameUI()
    {
        InGameUI.SetActive(false);
        EndGameUI.SetActive(true);
    }
}
