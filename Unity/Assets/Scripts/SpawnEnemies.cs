using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab;
    private float timeMaxForSpawn = 5f;
    [SerializeField] private float[] possiblePosX;
    private float time;
    private int numOfStrongEnemies;

    private string difficulty;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = MainMenu.difficulty;
        if(difficulty == "easy")
            timeMaxForSpawn = 7.5f;
        else if(difficulty == "medium")
            timeMaxForSpawn = 5.5f;
        else if(difficulty == "hard")
            timeMaxForSpawn = 3.5f;
    }

   

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        
        if(time < 0 && difficulty == "easy")
        {
            float x = possiblePosX[Random.Range(0, possiblePosX.Length)];
            Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);

            GameObject gb = Instantiate(prefab[0], pos, prefab[0].transform.rotation)  as GameObject;
            Destroy(gb, 15);

            time = Random.Range(timeMaxForSpawn - timeMaxForSpawn/4, timeMaxForSpawn + timeMaxForSpawn/4);
        }
        else if(difficulty == "medium" && time < 0)
        {
            float x = possiblePosX[Random.Range(0, possiblePosX.Length)];
            Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);
            int rand = Random.Range(0,2);

            GameObject gb = Instantiate(prefab[rand], pos, prefab[rand].transform.rotation) as GameObject;
            Destroy(gb, 15);

            time = Random.Range(timeMaxForSpawn - timeMaxForSpawn/4, timeMaxForSpawn + timeMaxForSpawn/4);

            numOfStrongEnemies += 1;
        }
        else if(difficulty == "hard" && time < 0)
        {       
            float x = possiblePosX[Random.Range(0, possiblePosX.Length)];
            Vector3 pos = new Vector3(x, transform.position.y, transform.position.z);
            int rand = Random.Range(0,2);

            GameObject gb = Instantiate(prefab[rand], pos, prefab[rand].transform.rotation) as GameObject;
            Destroy(gb, 15);

            time = Random.Range(timeMaxForSpawn - timeMaxForSpawn/4, timeMaxForSpawn + timeMaxForSpawn/4);

            numOfStrongEnemies += 2;
        }
    }
}
