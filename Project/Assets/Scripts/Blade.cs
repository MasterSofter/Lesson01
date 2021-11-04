using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Blade : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private Camera cameraMain;
    private CircleCollider2D circleCollider2D;

    [SerializeField] private GameObject trailBlade;

    public Vector2 DirectionBlade;
 
    private void StartCutting() {
        trailBlade.SetActive(true);
        circleCollider2D.enabled = true;
    }

    private void StopCutting() {
        circleCollider2D.enabled = false;
        trailBlade.SetActive(false);
    }

    private void Start()
    {
        trailBlade.SetActive(false);
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.isKinematic = true;
        cameraMain = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCutting();
        else if (Input.GetMouseButtonUp(0))
            StopCutting();

        Vector2 previousPosition = rigidBody2D.position;
        Vector2 newPosition = cameraMain.ScreenToWorldPoint(Input.mousePosition); ;
        DirectionBlade = (newPosition - previousPosition).normalized;
        rigidBody2D.position = newPosition;
    }
}
