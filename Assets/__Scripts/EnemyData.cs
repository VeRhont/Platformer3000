using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public float[] Healths;
    public float[] MaxHealths;
    public float[] PositionsX;
    public float[] PositionsY;
    public float[] PositionsZ;

    public EnemyData(GameObject[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i].GetComponent<Enemy>();
            Healths[i] = enemy.Health;
            MaxHealths[i] = enemy.MaxHealth;

            var position = enemies[i].transform.position;
            PositionsX[i] = position.x;
            PositionsY[i] = position.y;
            PositionsZ[i] = position.z;
        }        
    }
}