using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class <c>GameMan</c> is the mastermind from behind the scenes. It stores common game <c>functions</c> and <c>core</c> variables. 
/// The <c>UI</c> will get notified of any changes in the game's core variables and update itself accordingly.
/// </summary>
public class GameMan : MonoBehaviour
{
    private UIManager UI;
    private AudioManager AudioManager;

    private int highScore;
    private int score;
    private int coins;
    private GameState gameState;

    public GameState State
    {
        get { return gameState; }

        set
        {
            gameState = value;

            if (gameState == GameState.Ended)
            {
                AudioManager.PlayDeathFX();
                UI.ShowEndGameUI();
            }
        }
    }

    public enum GameState
    {
        InProgress,
        Ended
    }

    public static GameMan Instance { get; private set; }

    public int HighScore
    {
        get { return highScore; }

        private set
        {
            highScore = value;
            UI.UpdateHighScoreText(highScore);
        }
    }
    public int Coins
    {
        get { return coins; }

        private set
        {
            coins = value;
            UI.UpdateCoinsText(coins);
        }
    }

    public int Score
    {
        get { return score; }

        private set
        {
            score = value;
            UI.UpdateScoreText(score);
            UI.UpdateScoreTextEG(score);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.orientation = ScreenOrientation.AutoRotation;

        gameState = GameState.InProgress;
        UI = UIManager.Instance;
        AudioManager = AudioManager.Instance;
        HighScore = PlayerPrefs.GetInt("Highscore", 0);
        Coins = PlayerPrefs.GetInt("Coins", 0);
    }

    public void IncrementScore(int amount)
    {
        if (gameState == GameState.InProgress)
        {
            Score += amount;

            if (HighScore < Score)
            {
                PlayerPrefs.SetInt("Highscore", Score);
                HighScore = Score;
            }
        }
    }
    public void IncrementCoins(int amount)
    {
        if (gameState == GameState.InProgress)
        {
            Coins += amount;
            AudioManager.PlayCoinFX();
            PlayerPrefs.SetInt("Coins", Coins);
        }
    }
}
