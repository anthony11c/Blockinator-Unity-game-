using UnityEngine;
using System.Collections;

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
    private Direction _rotationDirection;
    private Vector3 _pivot;
    private float _totalRotation;
    private Vector3 _axis;
    private Vector3 _scale;

    //zvuk
    AudioSource audioSource;
    public float pocetniPitch = 1.36f;


    void Start()
    {
        _moving = false;
        _scale = transform.localScale / 2.0f;
       
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = pocetniPitch;
    }

    void Update()
    {
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

    }

    void Rotate(Direction direction)
    {
        _rotationDirection = direction;
        _moving = true;
        _totalRotation = 0;
        zvukKretanja();

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
}