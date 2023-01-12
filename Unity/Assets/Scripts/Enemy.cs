using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float currentSpeed = 6.5f;
    private float time ;
    private float accelerationTime = 60;
    
    [SerializeField] private MeshFilter[] meshFilters;

    public TrainControl.Forms forms;

    private string difficulty;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = MainMenu.difficulty;
        if(difficulty == "easy")
            currentSpeed = 5f;
        else if(difficulty == "medium")
            currentSpeed = 8f;
        else if(difficulty == "hard")
            currentSpeed = 12f;
        int rnd = Random.Range(0, meshFilters.Length);

        if(rnd == 0) GoTocube();
        else if(rnd == 1) GoToSphere();
        else if(rnd == 2) GoToCylinder();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * -currentSpeed * Time.deltaTime;
    }


    public void GoTocube()
    {
        forms = TrainControl.Forms.cube;
        GetComponent<MeshFilter>().sharedMesh = meshFilters[0].sharedMesh;
    }

    public void GoToSphere()
    {
        forms = TrainControl.Forms.sphere;
        GetComponent<MeshFilter>().sharedMesh = meshFilters[1].sharedMesh;
    }

    public void GoToCylinder()
    {
        forms = TrainControl.Forms.cylinder;
        GetComponent<MeshFilter>().sharedMesh = meshFilters[2].sharedMesh;
    }
}
