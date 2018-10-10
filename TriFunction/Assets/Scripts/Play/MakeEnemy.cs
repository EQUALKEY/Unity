using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeEnemy : MonoBehaviour {

    public GameObject[] Enemy = new GameObject[6]; // sin,sec,tan,cos,cosec,cotan

    private void Start()
    {
        Make_Enemy();
    }

    void Make_Enemy() 
    {
        StartCoroutine("Create_Enemy_Controller");
    }

    IEnumerator Create_Enemy_Controller() // Create_Enemy 관리해줌.
    {
        
        int EnemyType = Random.Range(0, 6);
        Create_Enemy(EnemyType,1f);
        yield return new WaitForSeconds(3f);
        StartCoroutine("Create_Enemy_Controller");
    }
    void Create_Enemy(int EnemyType, float velocity) // EnemyType이랑 velocity넣으면 적 만들어줌.
    {
        float PositionEdge = Random.Range(-1f * Mathf.PI, Mathf.PI); // 360도 방향에서 랜덤 생성
        GameObject newEnemy = Instantiate(Enemy[EnemyType], new Vector3(10f * Mathf.Cos(PositionEdge), 10f * Mathf.Sin(PositionEdge)), new Quaternion(0f,0f,0f,1f));
        newEnemy.GetComponent<EnemyBehaviour>().Velocity = velocity;
    }
    
}
