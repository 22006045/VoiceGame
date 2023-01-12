using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float timeMaxForSpawn;
    [SerializeField] private float[] possiblePosX;
    private float time;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if(time < 0)
        {
            float x = possiblePosX[Random.Range(0, possiblePosX.Length)];
            Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);

            GameObject gb = Instantiate(prefab, pos, prefab.transform.rotation);
            Destroy(gb, 15);

            time = Random.Range(timeMaxForSpawn - timeMaxForSpawn/4, timeMaxForSpawn + timeMaxForSpawn/4);
        }
    }
}
