using UnityEngine;

public class Score : Singleton<Score>
{
    int score = 0;

    public int LoseScore(int losedScore)
    {
        score -= losedScore;
        return score;
    }

    public int AddScore(int newscore)
    {
        score += newscore;
        return score;
    }

    //-------------------------------------------------------------
    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newscore)
    {
        score = newscore;
    }
}
