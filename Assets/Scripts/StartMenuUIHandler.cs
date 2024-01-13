using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUIHandler : MonoBehaviour
{
    public InputField InputField;
    public Text BestScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        InputField.onEndEdit.AddListener(delegate { LockInput(InputField); } );

        LoadScore();
    }

    void LockInput(InputField inputField)
    {
        if (inputField.text.Length > 32)
        {
            Debug.Log("User name must be shorter than 32 characters!");
            return;
        }

        GameManager.Instance.CurrentUser = inputField.text;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void LoadScore()
    {
        GameManager.Instance.LoadScore();

        string userName = GameManager.Instance.BestUser;
        int bestScore = GameManager.Instance.BestScore;
        
        BestScoreText.text = userName + ": " + bestScore + " pts.";
    }

    public void ResetScore()
    {
        GameManager.Instance.ResetScore();

        LoadScore();
    }
}
