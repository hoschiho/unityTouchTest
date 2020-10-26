
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    private Touch touch;
    private float speedModifier, step;

    private bool tap, isDraggig, swipeLeft, swipeRight, swipeUp, swipeDown, swipe;
    private Vector2 startTouch, swipeDelta;
    private Vector3 targetPosition = new Vector3(0, 10, 0);
    public float speed = 1.0f;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;



    void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        speedModifier = 1;
        step = speedModifier * Time.deltaTime;
    }



    /*-------------------move object-----------------------
        // Update is called once per frame
        void Update()
        {
            {
                // Handle screen touches.
                if (Input.touchCount > 0)
                {
                    touch = Input.GetTouch(0);
                    Debug.Log("Touch detected");

                    // Move the cube if the screen has the finger moving.
                    if (touch.phase == TouchPhase.Moved)
                    {
                        transform.position = new Vector3(
                            transform.position.x + touch.deltaPosition.x * Time.deltaTime,
                            transform.position.y,
                            transform.position.z + touch.deltaPosition.y * Time.deltaTime);
                                            Debug.Log("Transform done");


                    }
                }
            }
        }
    }
    */
    // Update is called once per frame


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
        }

        tap = false;
        isDraggig = false;
        swipeRight = false;
        swipeLeft = false;
        swipeUp = false;
        swipeDown = false;

        float step = speed * Time.deltaTime; // calculate distance to move




        // Handle screen touches.
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {

                startTouch = touch.position; // set start position
                Debug.Log("start Touch is " + startTouch);
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                swipeDelta = Vector2.zero;
                swipeDelta = touch.position - startTouch; // calculate the differece between start & end position
                Debug.Log("end Touch is " + touch.position);
                Debug.Log("swipe Delta mag is " + swipeDelta.magnitude);

                // Did we cross the deadzone?
                if (swipeDelta.magnitude > 125) // A good value for a natural swipe
                {
                    Debug.Log("swipe detected");
                    swipe = true;

                    Reset();
                }
                                // else, just a tap.

                else
                {
                    tap = true;
                    Reset();

                }

                Reset();
                Debug.Log("touchPhase Ended / Canceled");
            }
        }



        if (swipe == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                swipe = false;
                transform.position = new Vector3(0, 0, 0);
            }
        }

        if (tap == true)
        {
            TakeDamage(20);

        }



    }


    void Reset()
    {
        startTouch = Vector2.zero;
        swipeDelta = Vector2.zero;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}