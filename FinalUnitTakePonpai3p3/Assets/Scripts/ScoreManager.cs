using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public static void AddPoints(int amount)
    {
        score += amount;
        Debug.Log("SCORE: " + score);
    }
}
