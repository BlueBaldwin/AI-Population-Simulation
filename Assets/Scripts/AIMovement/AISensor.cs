using System;
using System.Collections;
using System.Collections.Generic;
using UtilityAi;
using UnityEngine;

[ExecuteInEditMode]
public class AISensor : MonoBehaviour
{
    RabbitController rabbitController;
    
    // [SerializeField] private GameObject AiSensor;
    public float distance = 10;
    public float angle = 30;
    public float _height = 1.0f;
    public Color meshColor = Color.cyan;
    public int scanFrequency;
    public LayerMask detectionLayer;
    public LayerMask occlusionLayer;
    public List<GameObject> objectsWithinSensor = new List<GameObject>(); // A list to store all those agents within the sensors cone
    // Hash set does not allow duplicate entries
    public HashSet<Vector3> previousFoodLocations = new HashSet<Vector3>();

    private Collider[] _colliders = new Collider[50];   // Storing the objects of all within the sphere radius
    private Mesh _mesh;
    private int _count;
    private float _scanIntervall;
    private float _scanTimer;



    // Start is called before the first frame update
    void Start()
    {
        _scanIntervall = 1.0f / scanFrequency;
        rabbitController = GetComponentInParent<RabbitController>();
    }

    // Update is called once per frame
    void Update()
    {
        _scanTimer -= Time.deltaTime;
        if (_scanTimer < 0)
        {
            _scanTimer += _scanIntervall;
            Scan();
        }
    }

    private void Scan()
    {
        _count = Physics.OverlapSphereNonAlloc(transform.position, distance, _colliders, detectionLayer, QueryTriggerInteraction.Collide);
        objectsWithinSensor.Clear();
        for (int i = 0; i < _count; i++)
        {
            GameObject g = _colliders[i].gameObject;
            if (g == null)
            {
                Debug.LogError("GameObject is null!");
                continue;
            }
            
            if (IsInSight(g))
            {
                objectsWithinSensor.Add(g);
                // If the game object is a food object, move to it
                if (g.CompareTag("Food"))
                {
                    rabbitController.SetFoodObject(g);
                    previousFoodLocations.Add(g.transform.position);
                }
            }
        }
    }


    private bool IsInSight(GameObject g)
    {
        Vector3 origin = transform.position;
        Vector3 destination = g.transform.position;
        Vector3 direction = destination - origin;
        // Testing the vertical height to see if it is greater or less than the sensor
        if(direction.y < 0 || direction.y > _height) 
        {
            return false;
        }

        // Checking if the agent is within the sensors angle - 0 out the y value to ensure its y position is not effecting the check
        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);
        if(deltaAngle > angle)
        {
            return false;
        }

        //Checking for occlusion
        origin.y += _height / 2;
        destination.y = origin.y;
        if(Physics.Linecast(origin, destination, occlusionLayer))
        {
            return false;
        }
        return true;

    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;      // each segment has 4 triangles, 2x far side 1x both top and bottom
        int numVertices = numTriangles * 3;             // 3 Vertices for every triangle

        Vector3[] vertices = new Vector3[numVertices]; // Filling with the total via the calc above
        int[] triangles = new int[numVertices];         // Ignoring index
        
        //Define the 6 points for the bottom and top triangles - these do not change as they are flexible depending on the angle of the wedge
        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
        
        //Top Triangle
        Vector3 topCenter = bottomCenter + new Vector3(0, _height, 0);
        Vector3 topLeft = bottomLeft + new Vector3(0, _height, 0);
        Vector3 topRight = bottomRight + new Vector3(0, _height, 0);

        int vert = 0; // Keeping track within the vert array

        // Left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft; 

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter; 
        
        // Right Side 
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;
        
        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        // SUBDIVISION - Recalculating the angle of the wedge
        float currentAngle = -angle; // Left side of the entire wedge
        float deltaAngle = (angle * 2) / segments; // Delta - the angle of segment
        // Loop over each of the segments incrementing the angle as we go - so first loop will make up the next two verts of the first segment until all inner segments are created.
        for (int i = 0; i < segments; i++)
        {
            // Each loop redefines the bottom left and bottom right as we loop through each segment
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topLeft = bottomLeft + new Vector3(0, _height, 0);
            topRight = bottomRight + new Vector3(0, _height, 0);
            
            // Far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;
            
            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;
            
            // Top 
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;
    
            //Bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;
            
            currentAngle += deltaAngle;
        }
        
        // Loop over number of vertices as there is no vertex sharing
        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        
        return mesh;
    }
    private void OnValidate()
    {
        _scanIntervall = 1.0f / scanFrequency;
        _mesh = CreateWedgeMesh();
    }
    private void OnDrawGizmos()
    {
        if (_mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(_mesh, transform.position, transform.rotation);
        }
        // Drawing a sphere from agents location
        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < _count; i++)
        {
            // Drawing a sphere at the objects location
            Gizmos.DrawWireSphere(_colliders[i].transform.position, 1.0f);
        }

        foreach (var item in objectsWithinSensor)
        {
             Gizmos.color = Color.red;
             Gizmos.DrawWireSphere(item.transform.position, 1.0f);
        }
    }
}
