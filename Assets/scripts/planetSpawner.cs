using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetSpawner : MonoBehaviour
{
    public float maxGap;
    float gap;
    public string[] planets;
    public float maxY;
    public float minY;
    public float rotZMax;
    public float rotZMin;
    Vector3 planetPosition;
    Vector3 planetRotation;
    Transform planet;

    // Start is called before the first frame update
    IEnumerator Start() {
        yield return new WaitForSeconds(2f);
        spawnFirstPlanet();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(planet != null){
            if(planet.position.x < gap)
            {
                planetPosition = new Vector3(transform.position.x,Random.Range(minY, maxY),transform.position.z);
                planetRotation = new Vector3(0,0,Random.Range(rotZMin, rotZMax));
                int randomNumber = Random.Range(0, planets.Length);
                GameObject planetClone = objectPooler.instance.spawnFromPool(planets[randomNumber], planetPosition, Quaternion.Euler(planetRotation));
                planet = planetClone.transform;
                float planetScale = Random.Range(0.5f, 1.0f);
                planet.localScale = new Vector3(planetScale,planetScale,planetScale);
                gap = Random.Range(0, maxGap);
            }
        }
    }

    public void spawnFirstPlanet()
    {
        planetPosition = new Vector3(transform.position.x,Random.Range(minY, maxY),transform.position.z);
        planetRotation = new Vector3(0,0,Random.Range(rotZMin, rotZMax));
        int randomNumber = Random.Range(0, planets.Length);
        GameObject planetClone = objectPooler.instance.spawnFromPool(planets[randomNumber], planetPosition, Quaternion.Euler(planetRotation));
        planet = planetClone.transform;
    }
}
