using UnityEngine;

public class Star : MonoBehaviour
{
    public Vector3 rotation;
    public float rotationSpeed = 90f;

    void Update()
    {
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            StarManager.Instance.CollectStar();
            Destroy(gameObject);
        }
    }
}
