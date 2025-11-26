using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform target;

    public Transform clampMin, clampMax;

    private Camera cam;
    private float halfWidth, halfHeight;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<PlayerController>().transform;

        clampMin.SetParent(null);
        clampMax.SetParent(null);

        cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth = cam.orthographicSize * cam.aspect; // 16/9 * 8
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        Vector3 clampedPos = transform.position;

        clampedPos.x = Mathf.Clamp(clampedPos.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth);
        clampedPos.y = Mathf.Clamp(clampedPos.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = clampedPos;
    }
}
