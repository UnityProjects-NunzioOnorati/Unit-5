using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody target;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    public float ySpawnPos = -6;
    private GameManager gameManager;
    public int pointValue = 0;
    public GameObject explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomSpawnPos();
        target = GetComponent<Rigidbody>();
        target.AddForce(RandomForce(), ForceMode.Impulse);
        target.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(),ForceMode.Impulse);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    Vector3 RandomForce(){
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque(){
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos(){
        return new Vector3(Random.Range(-xRange,xRange), ySpawnPos, -3);
    }

    private void OnMouseDown(){

        if(gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives();
        }

    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position,
            explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
}
