using UnityEngine;

public class BallPlayer : MonoBehaviour
{
    // Sensibilité de la souris
    public float sensitivity = 10.0f;

    // Référence à la caméra principale
    private Camera mainCamera;

    // Référence au Rigidbody de la boule
    private Rigidbody rb;

    public float vitesse;

    void Start()
    {
        // Récupère la caméra principale
        mainCamera = Camera.main;

        // Récupère le composant Rigidbody attaché à la boule
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Récupère la position de la souris en coordonnées d'écran
        Vector3 mousePosition = Input.mousePosition;

        // Convertit la position de la souris en coordonnées du monde
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane + 1.0f));

        // Calcule la direction du déplacement
        Vector3 direction = (worldPosition - transform.position);

        // Applique la vélocité à la boule en fonction de la direction et de la sensibilité
        rb.velocity = direction * sensitivity;
        vitesse = rb.velocity.y;
    }
}
