using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainControl : MonoBehaviour
{
    private VoiceRec voice;
    [SerializeField] private float speed; 
    [SerializeField] private float posXLineLeft, posXLineMidle, posXLineRight;
    private float atualPosX;
    [SerializeField] private AudioSource dieSound;
    [SerializeField] private AudioSource scoreSound;
    [SerializeField] private AudioSource movementSound;
    [SerializeField] private AudioSource transformSound;
    [SerializeField] private ParticleSystem dieParticles;
    [SerializeField] private ParticleSystem scoreParticles;
    [SerializeField] private ParticleSystem[] textParticles;
    [SerializeField] private ParticleSystem transformParticles;

    public enum TrainPos
    {
        left,
        midle,
        right
    }

    public TrainPos trainPos;

    public enum Forms
    {
        sphere,
        cube,
        cylinder
    }
    public Forms forms;
    [SerializeField] private MeshFilter[] meshFilters;
    [SerializeField] private MeshFilter mesh;


    [SerializeField] private ScoreControl scoreControl;

    [SerializeField] private GameObject DeadAnimation;

    // Start is called before the first frame update
    void Start()
    {
        voice = FindObjectOfType<VoiceRec>();
        trainPos = TrainPos.midle;
        forms = Forms.cube;

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

        if(Input.GetKeyDown(KeyCode.Q)) GoTocube();
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
        if(forms == Forms.cube)
        {
            mesh.sharedMesh = meshFilters[0].sharedMesh;
        }
        else if(forms == Forms.sphere)
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
        if(trainPos != TrainPos.left)
            movementSound.Play();
        trainPos = TrainPos.left;
    }

    public void GoToMid()
    {
        if(trainPos != TrainPos.midle)
            movementSound.Play();
        trainPos = TrainPos.midle;
    }

    public void GoToRight()
    {
        if(trainPos != TrainPos.right)
            movementSound.Play();
        trainPos = TrainPos.right;
    }


    public void GoTocube()
    {
        if(forms != Forms.cube)
        {
            transformParticles.Play();
            transformSound.Play();
        }
        forms = Forms.cube;
    }

    public void GoToSphere()
    {
        if(forms != Forms.sphere)
        {
            transformParticles.Play();
            transformSound.Play();
        }
        forms = Forms.sphere;
    }

    public void GoToCylinder()
    {
        if(forms != Forms.cylinder)
        {
            transformParticles.Play();
            transformSound.Play();
        }
        forms = Forms.cylinder;
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("teste");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if(enemy.forms == forms)
            {
                int newScore = Random.Range(2, 5);
                scoreControl.Score += newScore;
                scoreSound.Play();
                scoreParticles.Play();
                int randomChoice = Random.Range(0,3);
                textParticles[randomChoice].Play();
                Destroy(other.gameObject);
                
            }
            else
            {
                // DIES
                dieSound.Play();
                dieParticles.Play();
                StartCoroutine("Dead");
                DeadAnimation.SetActive(true);
            }
        }
        if(other.tag == "Bullet" && voice.shieldCount < 1)
        {
            dieSound.Play();
            dieParticles.Play();
            StartCoroutine("Dead");
            DeadAnimation.SetActive(true);
        }
        if(other.tag == "Bullet" && voice.shieldCount == 1)
        {
            other.GetComponent<SphereCollider>().isTrigger = false;
            Destroy(other.gameObject);
            StartCoroutine(voice.ActivateUI(voice.shieldOff, 2f));
            voice.shieldCount = 0;
        }

        if(other.tag == "score")
        {
            scoreControl.Score += 1;
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.85f);
        SceneManager.LoadScene("Dead");
    }
}
