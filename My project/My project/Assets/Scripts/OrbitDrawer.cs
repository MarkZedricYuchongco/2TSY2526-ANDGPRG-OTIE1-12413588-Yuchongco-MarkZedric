using UnityEngine;

public class OrbitDrawer : MonoBehaviour
{
    [SerializeField] Transform sunObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        // 1. Setup the Line appearance
        line.startWidth = 1f;
        line.endWidth = 1f;
        line.useWorldSpace = true;
        line.loop = true; // Connects the last point to the first

        // 2. Set how many "segments" make up the circle (higher = smoother)
        int segments = 100;
        line.positionCount = segments;

        // 3. Calculate the distance (radius) between the planet and the sun
        float radius = Vector3.Distance(transform.position, sunObject.position);

        // 4. Calculate the position of each point
        for (int i = 0; i < segments; i++)
        {
            // Convert the segment index into an angle (0 to 360 degrees)
            float time = (float)i / segments;
            float angle = time * 2 * Mathf.PI;

            // Use Math to find X and Z (Trigonometry)
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // Set the point relative to the Sun's center
            Vector3 point = new Vector3(x, 0, z) + sunObject.position;
            line.SetPosition(i, point);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
