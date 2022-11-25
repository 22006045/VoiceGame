using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainControl : MonoBehaviour
{
    [SerializeField] private float speed; 
    [SerializeField] private float posXLineLeft, posXLineMidle, posXLineRight;
    private float atualPosX;

    public enum TrainPos
    {
        left,
        midle,
        right
    }

    private TrainPos trainPos;

    public enum Forms
    {
        esfera,
        cubo,
        cylinder
    }
    private Forms forms;
    [SerializeField] private MeshFilter[] meshFilters;
    [SerializeField] private MeshFilter mesh;


    [SerializeField] private ScoreControl scoreControl;

    // Start is called before the first frame update
    void Start()
    {
        trainPos = TrainPos.midle;
        forms = Forms.cubo;

        atualPosX = posXLineMidle;
    }

    // Update is called once per frame
    void Update()
    {
        MovementTrain();
        UpdateForm();

        if(Input.GetKeyDown(KeyCode.A)) GoToLeft();
        if(Input.GetKeyDown(KeyCode.S)) GoToMid();
        if(Input.GetKeyDown(KeyCode.D)) GoToRight();

        if(Input.GetKeyDown(KeyCode.Q)) GoToCubo();
        if(Input.GetKeyDown(KeyCode.W)) GoToSphere();
        if(Input.GetKeyDown(KeyCode.E)) GoToCylinder();
    }


    private void MovementTrain()
    {
        if(trainPos == TrainPos.midle)
        {
            atualPosX = posXLineMidle;
        }    
        else if(trainPos == TrainPos.left)
        {
            atualPosX = posXLineLeft;
        } 
        else if(trainPos == TrainPos.right)
        {
            atualPosX = posXLineRight;
        }   
        
        Vector3 goTo = new Vector3(atualPosX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, goTo, Time.deltaTime * speed);
    }

    private void UpdateForm()
    {
        if(forms == Forms.cubo)
        {
            mesh.sharedMesh = meshFilters[0].sharedMesh;
        }
        else if(forms == Forms.esfera)
        {
            mesh.sharedMesh = meshFilters[1].sharedMesh;
        }
        else if(forms == Forms.cylinder)
        {
            mesh.sharedMesh = meshFilters[2].sharedMesh;
        }
    }

    public void GoToLeft()
    {
        trainPos = TrainPos.left;
    }

    public void GoToMid()
    {
        trainPos = TrainPos.midle;
    }

    public void GoToRight()
    {
        trainPos = TrainPos.right;
    }


    public void GoToCubo()
    {
        forms = Forms.cubo;
    }

    public void GoToSphere()
    {
        forms = Forms.esfera;
    }

    public void GoToCylinder()
    {
        forms = Forms.cylinder;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(enemy.forms == forms)
            {
                scoreControl.Score += Random.Range(2, 5);
                Destroy(other.gameObject);
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "score")
        {
            scoreControl.Score += 1;
        }
    }
}
