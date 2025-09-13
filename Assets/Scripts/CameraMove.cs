using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float offset;
    [SerializeField] float positionY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null) throw new System.NullReferenceException("Camera Move:target");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + offset, positionY, transform.position.z);
    }
}
