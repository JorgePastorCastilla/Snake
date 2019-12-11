using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{

    [SerializeField]
    private GameObject snakeBodyPrefab;
    [SerializeField]
    private GameObject foodPrefab;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Canvas PauseMenuCanvas;

    private List<Transform> bodyList = new List<Transform>();
    private Vector3 disabledDirection;
    private Vector3 direction;
    public float speed = 0.4f;
    private bool canChangeDirection;
    private bool gameIsPaused;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        bodyList.Add(transform);
        direction = Vector3.right;
        disabledDirection = Vector3.left;
        StartCoroutine("move");
    }

    // Update is called once per frame
    void Update()
    {
        if ( ( Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) ) && !gameIsPaused)
        {
            pauseGame();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) && gameIsPaused)
            {
                resumeGame();
            }
            if ( (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ) && canChangeDirection && !gameIsPaused )
            {
                canChangeDirection = false;
                if (disabledDirection != Vector3.up)
                {
                    direction = Vector3.up;
                }

            }
            else if ( (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ) && canChangeDirection && !gameIsPaused )
            {
                canChangeDirection = false;
                if (disabledDirection != Vector3.down)
                {
                    direction = Vector3.down;
                }
            }
            else if ( (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ) && canChangeDirection && !gameIsPaused )
            {
                canChangeDirection = false;
                if (disabledDirection != Vector3.left)
                {
                    direction = Vector3.left;
                }
            }
            else if ( (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ) && canChangeDirection && !gameIsPaused )
            {
                canChangeDirection = false;
                if (disabledDirection != Vector3.right)
                {
                    direction = Vector3.right;
                }
            }
            disabledDirection = direction * (-1);
        }

    }

    IEnumerator move()
    {
        while (true)
        {
            canChangeDirection = true;
            for ( int i = 0; i < bodyList.Count; i++ )
            {
                if (i > 0) {
                    bodyList[bodyList.Count - i].position = bodyList[bodyList.Count - i - 1].position;
                }
            }
            bodyList[0].Translate(direction);
            yield return new WaitForSeconds(speed);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTRA EN TRIGGER");
        if (other.gameObject.tag == "Food")
        {
            gameManager.addScore();
            Debug.Log("ENTRA EN IF");
            Destroy(other.gameObject);
            Vector3 spawnPosition;
             if (bodyList.Count == 1)
            {
                spawnPosition = bodyList[bodyList.Count - 1].position - direction;
            }
            else
            {
                spawnPosition = bodyList[bodyList.Count - 1].position + (bodyList[bodyList.Count - 1].position - bodyList[bodyList.Count - 2].position);
            }
            GameObject newBodyPart = (GameObject) Instantiate(snakeBodyPrefab, spawnPosition, Quaternion.identity);
            bodyList.Add(newBodyPart.transform);
            Instantiate(foodPrefab, new Vector3( Random.Range(-14,14) , Random.Range(-9, 9), 0 ), Quaternion.identity);
            if(speed > 0.09) speed -= (speed * 0.10f);
        }else if (other.gameObject.tag == "Snake")
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }

    void pauseGame()
    {
        Time.timeScale = 0;
        PauseMenuCanvas.gameObject.SetActive(true);
        gameIsPaused = true;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        PauseMenuCanvas.gameObject.SetActive(false);
        gameIsPaused = false;
    }
    public void gotoMainMenu()
    {

    }

}
