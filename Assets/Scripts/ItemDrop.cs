using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public Item itemData;
    public float rotationSpeed;
    public float dropUpwardForce;
    public Rigidbody rb;

    public float groundDetectionDist;
    public LayerMask groundLm;

    private void Start ()
    {
        rb.AddForce(dropUpwardForce * Vector3.up, ForceMode.Impulse);
    }

    private void Update ()
    {
        // Collision:
        if (Physics.Raycast(transform.position, Vector3.down, groundDetectionDist, groundLm))
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter (Collider col)
    {
        if (col.transform.CompareTag("Player"))
        {
            // Player picks up item:
            InventorySystem.Instance.GetItem(itemData);

            Destroy(gameObject);
        }
    }
}
