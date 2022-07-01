using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private int _score;
    private TextMeshProUGUI _scoreTMP;

    private void Awake()
    {
        _scoreTMP = GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseScore(int increaseAmount)
    {
        _score += increaseAmount;
        _scoreTMP.text = _score.ToString();
    }
}