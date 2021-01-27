using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Direction
{
    North,
    South,
    East,
    West
}

public class CubeController : MonoBehaviour
{

    public float rotationSpeed = 250;

    private bool _moving;
    public bool prolaz;
    private Direction _rotationDirection;
    private Vector3 _pivot;
    private float _totalRotation;
    private Vector3 _axis;
    private Vector3 _scale;

    //zvuk
    AudioSource audioSource;
    public float pocetniPitch = 1.36f;

    //pokreti
    public int brojPokreta;
    public Text score;

    //timer
    public float timer;
    public Text timerText;

    void Start()
    {
        _moving = false;
        prolaz = false;
        _scale = transform.localScale / 2.0f;
       
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = pocetniPitch;

        brojPokreta = 0;
    }

    void Update()
    {
        povecajBrojSekundi();
        if (_moving)
        {
            float deltaRotation = rotationSpeed * Time.deltaTime * 2.0f;
            if (_totalRotation + deltaRotation >= 91)
            {
                deltaRotation = 91 - _totalRotation;
                _moving = false;
            }
            if ((_rotationDirection == Direction.West) || (_rotationDirection == Direction.North))
                transform.RotateAround(_pivot, _axis, deltaRotation);
            else transform.RotateAround(_pivot, _axis, -deltaRotation);

            _totalRotation += deltaRotation;
        }
        else if (Input.GetKeyUp(KeyCode.D)) Rotate(Direction.North);
        else if (Input.GetKeyUp(KeyCode.W)) Rotate(Direction.West);
        else if (Input.GetKeyUp(KeyCode.A)) Rotate(Direction.South);
        else if (Input.GetKeyUp(KeyCode.S)) Rotate(Direction.East);


        //povratak u menu s escapeom
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        //reset collidera nakon pada kroz rupu
        if (this.transform.position.y < -15)
        {
            this.GetComponent<BoxCollider>().enabled = true;
            
            //nestajanje lika
            if(this.transform.position.y < -45)
            {
                this.GetComponent<MeshRenderer>().enabled = false;
            }

        }
    }

    void Rotate(Direction direction)
    {
        _rotationDirection = direction;
        _moving = true;
        _totalRotation = 0;
        zvukKretanja();
        povecajBrojPokreta();

        switch (_rotationDirection)
        {
            case Direction.East:
                _pivot = transform.position + new Vector3(_scale.x, -_scale.y, 0);
                break;
            case Direction.West:
                _pivot = transform.position + new Vector3(-_scale.x, -_scale.y, 0);
                break;
            case Direction.North:
                _pivot = transform.position + new Vector3(0, -_scale.y, _scale.z);
                break;
            case Direction.South:
                _pivot = transform.position + new Vector3(0, -_scale.y, -_scale.z);
                break;
        }

        if ((_rotationDirection == Direction.East) || (_rotationDirection == Direction.West))
        {
            _axis = Vector3.forward;
            float temp = _scale.x;
            _scale.x = _scale.y;
            _scale.y = temp;
        }
        else
        {
            _axis = Vector3.right;
            float temp = _scale.z;
            _scale.z = _scale.y;
            _scale.y = temp;
        }

    }

    void zvukKretanja()
    {
        audioSource.pitch = Random.Range(pocetniPitch, pocetniPitch + 0.4f);
        audioSource.Play();
    }

    void povecajBrojPokreta()
    {
        brojPokreta += 1;
        score.GetComponent<Text>().text = brojPokreta.ToString();
        
    }

    void povecajBrojSekundi()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        
        timerText.GetComponent<Text>().text = timerText.text;
    }

    void OnCollisionEnter(Collision collision)
    {
        float pozicijaY = 0;

        if (collision.collider.name == "Kraj")
        {
            //provjera je li kraj nivoa
            pozicijaY = this.transform.position.y;
            print(pozicijaY);
            if (pozicijaY > 6.2)
            {
                this.GetComponent<BoxCollider>().enabled = false;
               // this.GetComponent<CubeController>().enabled = false;
                prolaz = true;
            }
            else
            {
               // print("pozicija ne valja, ona je "+ pozicijaY);
            }
        }


        //promijena ili restart nivoa
        string imeScene = SceneManager.GetActiveScene().name;

        if (collision.collider.name == "Prolaz" && prolaz == false)
        {
            SceneManager.LoadScene(imeScene);
        }
        else if (collision.collider.name == "Prolaz" && prolaz == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}