using UnityEngine;

public class BuildingScore : MonoBehaviour
{
    public int pointsGiven = 10;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            ScoreManager.AddPoints(pointsGiven);
            Debug.Log("Building hit! +" + pointsGiven + " points");
        }
    }
}
