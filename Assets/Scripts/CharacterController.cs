using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpForce = 10f;
    public Text scoreText;

    private Rigidbody cachedRigidbody;
    private GameObject[] pickups;

    private bool jumped = false;
    private int score;

    public void Start()
    {
        pickups = GameObject.FindGameObjectsWithTag( "Pickup" );

        cachedRigidbody = GetComponent<Rigidbody>();

        UpdateScore( 0 );
    }

    public void Update()
    {
        jumped = false;
        if( Input.GetButtonDown( "Jump" ) )
        {
            jumped = true;
        }
        if( Input.GetKeyDown( KeyCode.Escape ) )
        {
            for( int i = 0; i < pickups.Length; ++i )
            {
                UpdateScore( 0 );
                pickups[i].SetActive( true );
                Vector2 rand = Random.insideUnitCircle;
                Vector3 oldPosition = pickups[i].transform.position;
                Vector3 position = new Vector3( rand.x * 9f, oldPosition.y, rand.y * 9f );
                pickups[i].transform.position = position;
            }
        }
    }

    public void FixedUpdate()
    {
        float h = Input.GetAxis( "Horizontal" );
        float v = Input.GetAxis( "Vertical" );

        float j = 0f;
        if( jumped )
        {
            j = jumpForce;
        }
        cachedRigidbody.AddForce( h * speed, j, v * speed );
    }

    public void OnTriggerEnter( Collider other )
    {
        if( other.CompareTag( "Pickup" ) )
        {
            other.gameObject.SetActive( false );
            UpdateScore( score + 1 );
        }
    }

    private void UpdateScore( int newScore )
    {
        score = newScore;
        scoreText.text = "Score: " + score.ToString();
    }
}

