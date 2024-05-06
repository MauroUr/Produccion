using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    [SerializeField] private float BASE_SPEED;
    private int life = 100;

    [SerializeField] private float rotationSpeed = 5f;
    private float speed;
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private HUDManager hudManager;
    [SerializeField] private GameObject map;
    private Rigidbody2D rb;
    private Animator animator;
    private List<Color> allColors = new List<Color>() { Color.red, Color.blue, Color.yellow, Color.magenta };

    private float nitroFuel = 100f;
    private bool canUseTp = true;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void Start()
    {
        this.ChangeColor(allColors);
        speed = BASE_SPEED;
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        this.MoveToCursor();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && canUseTp)
        {
            this.Teletransport();
            canUseTp = false;
        }
        
        this.IsUsingNitro();
        hudManager.UpdateHUD(this.nitroFuel, this.life);
        
        if (life <= 0)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }
    public float GetPlayerBSpeed() { return BASE_SPEED; }
    public void TPIsReady() { canUseTp = true; }

    private void MoveToCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;

        rb.velocity = (this.transform.up).normalized * speed * Time.deltaTime * 50;

        Vector3 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle-90);

        if (targetRotation.z < transform.rotation.z)
            animator.SetBool("Turning_right", true);
        else if (targetRotation.z > transform.rotation.z)
            animator.SetBool("Turning_left", true);
        else
        {
            animator.SetBool("Turning_right", false);
            animator.SetBool("Turning_left", false);
        }

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

        hudManager.ResetTP();
    }

    private void IsUsingNitro()
    {
        if (Input.GetMouseButton(0) && nitroFuel > 0.5)
            nitroFuel -= 0.75f;

        if (Input.GetMouseButtonDown(0) && nitroFuel > 10)
        {
            speed *= 2;
            rotationSpeed *= 1.5f;
        }

        if (speed > BASE_SPEED && (Input.GetMouseButtonUp(0) || nitroFuel <= 0))
        {
            speed /= 2;
            rotationSpeed /= 1.5f;
        }

        if (nitroFuel <= 100)
            nitroFuel += 0.25f;
    }

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
                cubeSpawner.SpawnCube(cubeColor);

                List<Color> nextColorList = new List<Color>(allColors);
                nextColorList.Remove(cubeColor);
                ChangeColor(nextColorList);

                Destroy(collision.gameObject);
                hudManager.RecoverPlanetLife(2);
            }
        }
    }
}

