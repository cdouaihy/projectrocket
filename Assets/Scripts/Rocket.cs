using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;

    [SerializeField] Lives life;
    [SerializeField] Switch swi;
    [SerializeField] Switch swi2;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem mainEnginePart;
    [SerializeField] ParticleSystem deathPart;
    [SerializeField] ParticleSystem successPart;

    [SerializeField] bool isCol = true;
    enum State {Alive, Dying, Transcending};
    State state = State.Alive;



    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Escape))
        {
            Invoke("RestartFirstScene", 0.5f);
        }
            if (state == State.Alive)
        {
            Thrust();
            rotate();
        }
        if (Debug.isDebugBuild)
        {
            if (Input.GetKey(KeyCode.L))
            {
                Invoke("LoadNextScene", 1f);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCol = !isCol;
            }
        }
    }
    private void Thrust()
    {
        float upFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up * upFrame);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
            mainEnginePart.Play();

        }
        else
        {
            mainEnginePart.Stop();
            audioSource.Stop();
        }
    }
    private void rotate()
    {
        rigidBody.freezeRotation = true;


        float rotationFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(-Vector3.forward * rotationFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(Vector3.forward * rotationFrame);

        }
        rigidBody.freezeRotation = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive || !isCol)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                successPart.Play();
                Invoke("LoadNextScene", 1f);
                break;
            case "ExtraLife":
                Destroy(collision.gameObject);
                life.AddLife();
                break;
            case "SmallB":
                Destroy(collision.gameObject);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case "Switch":
                swi.turnOn();
                break;
            case "Switch2":
                swi2.turnOn();
                break;
            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
                deathPart.Play();
                if (life.getLife() == 0)
                {
                    Invoke("RestartFirstScene", 1f);

                }
                else
                {
                    life.RemoveLife();
                    Invoke("LoadSameScene", 1f);
                }
                break;
        }
    }

    private void RestartFirstScene()
    {
        SceneManager.LoadScene(0);
        if (life)
        {
            life.ResetLife();
        }
    }

    private void LoadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        int tot = SceneManager.sceneCountInBuildSettings;
        int num = SceneManager.GetActiveScene().buildIndex;
        num += 1;
        SceneManager.LoadScene((num) % tot );
    }
}
