using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;   
    
    void Start()
    {
        Vector3 position =  new Vector3(Random.Range(-10,10),1.5f,Random.Range(-10,10));
        Instantiate(enemyPrefab, position, Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
