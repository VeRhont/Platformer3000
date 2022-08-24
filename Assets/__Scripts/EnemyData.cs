using UnityEngine;

[System.Serializable]
public class EnemyData : MonoBehaviour
{
    public float[] Healths;
    public float[] MaxHealths;
    public float[] PositionsX;
    public float[] PositionsY;
    public float[] PositionsZ;

    public EnemyData(Enemy[] enemies)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Healths[i] = enemies[i].Health;
            MaxHealths[i] = enemies[i].MaxHealth;

            var position = enemies[i].transform.position;
            PositionsX[i] = position.x;
            PositionsY[i] = position.y;
            PositionsZ[i] = position.z;
        }        
    }
}