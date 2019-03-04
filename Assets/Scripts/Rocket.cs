using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour {
    Rigidbody rigidBody;
    AudioSource audioSource;

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

            transform.Rotate(Vector3.forward * rotationFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(-Vector3.forward * rotationFrame);

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
            default:
                state = State.Dying;
                audioSource.Stop();
                audioSource.PlayOneShot(deathSound);
                deathPart.Play();
                Invoke("RestartFirstScene", 1f);
                break;
        }
    }

    private void RestartFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int tot = SceneManager.sceneCountInBuildSettings;
        int num = SceneManager.GetActiveScene().buildIndex;
        num += 1;
        SceneManager.LoadScene((num) % tot );
    }
}
