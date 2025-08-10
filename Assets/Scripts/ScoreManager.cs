using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TMP_Text scoreText;
    int score = 0;

    public void IncreaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = score.ToString();

    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
