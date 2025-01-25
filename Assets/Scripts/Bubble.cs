using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public enum BubbleType { Empty, Fire, Air, Water, Earth };
    public BubbleType bubbleType;

    public float spawnRangeX = 10f;
    public float spawnRangeZ = 10f;
    public float defaultY = 2f;
    public float animAmplitude = .5f;
    public float animSpeed = 2f;
    
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //logica para animação    
        float newY = defaultY + Mathf.Sin(Time.time * animSpeed) * animAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);

           Invoke(nameof(Respawn), 0.5f);
        }
    }

    private void Respawn()
    {
        float newX = Random.Range(-spawnRangeX, spawnRangeX);
        float newZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        startPosition = new Vector3(newX, defaultY, newZ);
        transform.position = startPosition; 

        gameObject.SetActive(true);
    }

    
}
