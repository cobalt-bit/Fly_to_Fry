using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float offset;
    [SerializeField] float positionY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject obj in objects)
            {
                if (!obj.activeInHierarchy) continue;
                target = obj.transform;
                break;
            }
        }
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
