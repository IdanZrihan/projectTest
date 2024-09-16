using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawn : MonoBehaviour
{
    public GameObject[] allySpawn; 
    public GameObject[] enemySpawn; 

    void Start()
    {
        FriendSpawn(); 
        EnemySpawn();
    }

    void Update()
    {
    }

    public void FriendSpawn()
    {
        for (int i = 0; i < allySpawn.Length; i++)
        {
            // ערך רנדומלי בין 150 ל-165 על ציר ה-X
            float randomX = Random.Range(150f, 165f);

            // שמירה על Y ו-Z כמו במיקום הנוכחי של האובייקט
            Vector3 randomPosition = new Vector3(randomX, allySpawn[i].transform.position.y, 20);

            // עדכון המיקום של האובייקט למיקום החדש
            allySpawn[i].transform.position = randomPosition;
        }
    }

   public void EnemySpawn()
{
    for (int f = 0; f < enemySpawn.Length; f++)
    {
        // יוצרים מיקום רנדומלי בציר ה-X
        float randomXx = Random.Range(100f, 120f);

        // יוצרים וקטור מיקום חדש עבור האויב
        Vector3 randomPositionNew = new Vector3(randomXx, enemySpawn[f].transform.position.y, 980);

        // יוצרים את האויב במיקום החדש
        Instantiate(enemySpawn[f], randomPositionNew, Quaternion.identity);
    }
}

}
