using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreNumberText;
    /// <summary>
    /// Updates the score display with the given score value.
    /// </summary>
    public void UpdateScore(int score)
    {
        scoreNumberText.text = score.ToString("0000");
    
    }

}
