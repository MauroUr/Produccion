using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnDeath;
    public UnityEvent OnPixelPicked;
    public UnityEvent OnTPUsed;
    [SerializeField] private float BASE_SPEED;
    private int life = 100;

    [SerializeField] private float rotationSpeed = 5f;
    private float speed;
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private GameObject map;
    private Rigidbody2D rb;
    private List<Color> allColors;

    private float nitroFuel = 100f;
    private bool canUseTp = true;

    [SerializeField] private bool level2 = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OnDeath = new UnityEvent();
        OnPixelPicked = new UnityEvent();
        OnTPUsed = new UnityEvent();
    }
    private void Start()
    {
        allColors = cubeSpawner.cubeColors;
        this.ChangeColor(allColors);
        speed = BASE_SPEED;
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        this.MoveToCursor();
    }
    private void Update()
    {
        if (GameController.Instance.isPaused)
            return;

        if(Input.GetKeyDown(KeyCode.X) && canUseTp)
            this.Teletransport();
        
        this.IsUsingNitro();
        
        if (life <= 0)
            OnDeath.Invoke();
    }


    private void MoveToCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        rb.velocity = (this.transform.up).normalized * speed * Time.deltaTime * 50;

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle-90);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    private void ChangeColor(List<Color> colorList)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = colorList[Mathf.RoundToInt(Random.Range(0, colorList.Count))];
    }

    private void Teletransport()
    {
        Vector3 localPlayerPosition = this.map.transform.InverseTransformPoint(transform.position);

        Vector3 oppositeLocalPosition = new Vector3(-localPlayerPosition.x, -localPlayerPosition.y, localPlayerPosition.z);

        transform.position = this.map.transform.TransformPoint(oppositeLocalPosition);

        canUseTp = false;
        OnTPUsed.Invoke();
    }

    private void IsUsingNitro()
    {
        if (Input.GetMouseButton(0) && nitroFuel > 0.5)
            nitroFuel -= 0.75f;

        if (Input.GetMouseButtonDown(0) && nitroFuel > 10)
        {
            speed *= 2;
            rotationSpeed *= 1.5f;
            AudioManager.instance.PlaySound("Nitro");
        }

        if (speed > BASE_SPEED && (Input.GetMouseButtonUp(0) || nitroFuel <= 0))
        {
            speed /= 2;
            rotationSpeed /= 1.5f;
        }

        if (nitroFuel <= 100)
            nitroFuel += 0.25f;

    }

    public float[] GetPlayerStatus()
    {
        float[] status = { this.nitroFuel, this.life };
        return status;
    }

    public void TPIsReady() { canUseTp = true; }
    public float GetPlayerBSpeed() { return BASE_SPEED; }
    public void TakeDamage(int amount)
    {
        life -= amount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.layer == 3)
        {
            Color cubeColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

            if (cubeColor == this.gameObject.GetComponent<SpriteRenderer>().color)
            {
                List<Color> nextColorList = new List<Color>(allColors);
                nextColorList.Remove(cubeColor);

                if (!level2)
                {
                    ChangeColor(nextColorList);
                    cubeSpawner.SpawnCube(cubeColor);
                }
                else
                    cubeSpawner.SpawnCube(nextColorList[Random.Range(0,nextColorList.Count)]);

                Destroy(collision.gameObject);
                OnPixelPicked.Invoke();
                AudioManager.instance.PlaySound("PickPixel");
            }
        }

        if (collision != null && collision.gameObject.layer == 7)
        {
            List<Color> colorDePortal = new List<Color>();
            colorDePortal.Add(collision.gameObject.GetComponent<SpriteRenderer>().color);
            ChangeColor(colorDePortal);
        }
    }
}