using UnityEngine;

public class PositionAtFaceScreenSpace : MonoBehaviour
{
    private float _camDistance;

    void Start()
    {
        _camDistance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    void Update()
    {
        if (opencv_test_csharp.NormalizedFacePositions.Count == 0)
            return;

        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(opencv_test_csharp.NormalizedFacePositions[0].x, opencv_test_csharp.NormalizedFacePositions[0].y, _camDistance));
    }
}