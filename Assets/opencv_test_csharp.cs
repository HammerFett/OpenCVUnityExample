using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class opencv_test_csharp : MonoBehaviour
{
    public static List<Vector2> NormalizedFacePositions { get; private set; }
    public static Vector2 CameraResolution;

    private const int DetectionDownScale = 1;
    private bool _ready;
    private int _maxFaceDetectCount = 5;
    private CvCircle[] _faces;
    // Start is called before the first frame update
    void Start()
    {
        int camWidth = 0, camHeight = 0;
        int result = OpenCVInterop.Init(ref camWidth, ref camHeight);
        if (result < 0)
        {
            Debug.LogWarningFormat("Failed to initialize DLL");
            return;
        }
        CameraResolution = new Vector2(camWidth, camHeight);
        _faces = new CvCircle[_maxFaceDetectCount];
        NormalizedFacePositions = new List<Vector2>();
        OpenCVInterop.SetScale(DetectionDownScale);
        _ready = true;
    }

    void OnApplicationQuit()
    {
        if (_ready)
        {
            OpenCVInterop.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_ready)
        {
            return;
        }
        int detectedFaceCount = 0;
        unsafe
        {
            fixed (CvCircle* outFaces = _faces)
            {
                OpenCVInterop.Detect(outFaces, _maxFaceDetectCount, ref detectedFaceCount);
            }
        }
        NormalizedFacePositions.Clear();
        for (int i = 0; i < detectedFaceCount; i++)
        {
            NormalizedFacePositions.Add(new Vector2((_faces[i].X * DetectionDownScale) / CameraResolution.x, 1f - ((_faces[i].Y * DetectionDownScale) / CameraResolution.y)));
        }
    }
}

// Define the functions which can be called from the .dll.
internal static class OpenCVInterop
{
    [DllImport("OpenCV_for_Unity")]
    internal static extern int Init(ref int outCameraWidth, ref int outCameraHeight);

    [DllImport("OpenCV_for_Unity")]
    internal static extern int Close();

    [DllImport("OpenCV_for_Unity")]
    internal static extern int SetScale(int sc);

    [DllImport("OpenCV_for_Unity")]
    internal unsafe static extern void Detect(CvCircle* outFaces, int maxOutFacesCount, ref int outDetectedFacesCount);
}

[StructLayout(LayoutKind.Sequential, Size = 12)]
public struct CvCircle
{
    public int X, Y, Radius;
}
