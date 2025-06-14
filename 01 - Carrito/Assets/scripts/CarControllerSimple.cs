using System.Collections;
using UnityEngine;

public class CarControllerSimple : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 200f;

    // Turbo
    public float turboMultiplier = 2f;
    public float turboDuration = 2f;
    private bool isTurboActive = false;

    // Pinchos
    public float slowMultiplier = 0.5f;
    public float slowDuration = 2f;
    private bool isSlowed = false;
    private float normalSpeed;
    
    void Start()
    {
        normalSpeed = speed;
    }

    void Update()
    {
        // Movimiento adelante
        float moveInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * moveInput * speed * Time.deltaTime);

        // Rotación
        float turnInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * -turnInput * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entró a cualquiera");
        // Activar Turbo
        if (other.CompareTag("Turbo") && !isTurboActive)
        {
            StartCoroutine(ActivateTurbo());
            Destroy(other.gameObject); // Elimina el objeto del turbo al recogerlo
        }

        // Activar Pinchos
        if (other.CompareTag("Pinchos") && !isSlowed)
        {
            StartCoroutine(ActivateSlow());
            Destroy(other.gameObject); // Opcional: elimina el pincho al activarlo
        }
        
    }

    IEnumerator ActivateTurbo()
    {
        isTurboActive = true;
        speed *= turboMultiplier;

        yield return new WaitForSeconds(turboDuration);

        speed = normalSpeed;
        isTurboActive = false;
    }

    IEnumerator ActivateSlow()
    {
        isSlowed = true;
        speed *= slowMultiplier;

        yield return new WaitForSeconds(slowDuration);

        speed = normalSpeed;
        isSlowed = false;
    }
}