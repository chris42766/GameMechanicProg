using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CheckCollision : MonoBehaviour
{
    public GameObject obg;
    public BallVertices balls;
    public List<GameObject> groupOfBalls;
    public List<GameObject> groupOfTargetsVertices;
    //public List<GameObject> groupOfTargets;
    // public BallVertices ballvertices;
    // Start is called once before the firBallst execution of Update after the MonoBehaviour is created
    void Start()
    {
        balls=obg.GetComponent<BallVertices>();
       // groupOfBalls = GameObject.FindGameObjectsWithTag("Ball").ToList();
       // groupOfTargets= GameObject.FindGameObjectsWithTag("Target").ToList();
        // ballvertices =GameObject.FindGameObjectWithTag("Ball").GetComponent<BallVertices>();
    }

    // Update is called once per frame
    void Update()
    {
        groupOfBalls = GameObject.FindGameObjectsWithTag("Ball").ToList();
        groupOfTargetsVertices = GameObject.FindGameObjectsWithTag("Target").ToList();
       // groupOfTargets= GameObject.FindGameObjectsWithTag("Target").ToList();


        /* balls = obg.GetComponent<BallVertices>();
         for (int j = 0; j < groupOfTargets.Count; j++)
             {
                 TargetVertices b = groupOfTargets[j].GetComponent<TargetVertices>();

                 CheckSAT(balls.vertices, b.vertices);

                 if (CheckSAT(balls.vertices, b.vertices))
                 {
                     Debug.Log("Collided from Gamemechanic stuff");
                 }
             }*/

    }

    void LateUpdate()
    {
        for (int i = 0; i < groupOfBalls.Count; i++)
        {
            BallVertices a = groupOfBalls[i].GetComponent<BallVertices>();

            for (int j = 0; j < groupOfTargetsVertices.Count; j++)
            {
                
                TargetVertices b = groupOfTargetsVertices[j].GetComponent<TargetVertices>();
                TutorialTarget c = groupOfTargetsVertices[j].GetComponent<TutorialTarget>();

                CheckSAT(a.vertices, b.vertices);

                if (CheckSAT(a.vertices, b.vertices))
                {
                    Debug.Log("Collided from Gamemechanic stuff");
                    c.changeTarget();
                    Destroy(a.gameObject);
                }  
            }
        }
    }



    Vector2 Subtract(Vector2 a, Vector2 b)
{
    return new Vector2(a.x - b.x, a.y - b.y);
}

// Add vectors


Vector2 Perpendicular(Vector2 v)
{
        return new Vector2(-v.y, v.x);
}

// Find the length of a vector
float Length(Vector2 v)
{
    // use Pythagoras theorem
    return Mathf.Sqrt(v.x * v.x + v.y * v.y);
}

Vector2 Normalize(Vector2 v, string num)
{
    // find the vector length
    float len = Length(v);
    
    // divide x and y by the length
    return new Vector2(v.x / len, v.y / len) ;
    
}

float DotSAT(Vector2 a, Vector2 b)
{
    // dot product formula
    return a.x * b.x + a.y * b.y;
}
    void GetAxes(Vector2[] vertices, Vector2[] axes, string num)
    {
        // build the first edge using vertex 0 and vertex 1
        Vector2 edge1 = Subtract(vertices[1], vertices[0]);

        // build the second edge using vertex 1 and vertex 2
        Vector2 edge2 = Subtract(vertices[2], vertices[1]);

        // show how the edges are built
      

        // turn each edge into a perpendicular vector
        Vector2 perp1 = Perpendicular(edge1);
        Vector2 perp2 = Perpendicular(edge2);

        

        // normalize the perpendicular vectors to make unit axes
        axes[0] = Normalize(perp1, "Edge 1 ");
        axes[1] = Normalize(perp2, "Edge 2 ");

     
    }

    void ProjectShape(Vector2 []vertices, Vector2 axis,out float minProj, out float maxProj)
    {
        // project the first vertex onto the axis
        minProj = DotSAT(vertices[0], axis);

        // at the start, max is the same as min
        maxProj = minProj;

       

   

        // loop through the remaining 3 vertices
        for (int i = 1; i < 4; i++)
        {
            // project this vertex onto the axis
            float p = DotSAT(vertices[i], axis);

         

            // if this projection is smaller, update min
            if (p < minProj)
            {
                minProj = p;

            }

            // if this projection is larger, update max
            if (p > maxProj)
            {
                maxProj = p;

            }
        }


    }
    bool IntervalsOverlap(float minA, float maxA, float minB, float maxB)
    {
        // if A ends before B starts, no overlap
        if (maxA < minB)
        {
            return false;
        }

        // if B ends before A starts, no overlap
        if (maxB < minA)
        {
            return false;
        }

        // otherwise, they overlap
        return true;
    }
    /*bool CheckSAT(Vector2 []verticesA, Vector2 []verticesB)
    {
        Vector2 axesA[2];
        Vector2 axesB[2];

        GetAxes(verticesA, axesA, "First Box ");
        GetAxes(verticesB, axesB, "Second Box ");

     
        // test A's axes
        for (int i = 0; i < 2; i++)
        {
           
            float minA, maxA, minB, maxB;

            ProjectShape(verticesA, axesA[i], minA, maxA);
            ProjectShape(verticesB, axesA[i], minB, maxB);

            if (!IntervalsOverlap(minA, maxA, minB, maxB))
                return false;
        }


        // test B's axes
        for (int i = 0; i < 2; i++)
        {
        
            float minA, maxA, minB, maxB;

            ProjectShape(verticesA, axesB[i], minA, maxA);
            ProjectShape(verticesB, axesB[i], minB, maxB);

            if (!IntervalsOverlap(minA, maxA, minB, maxB))
                return false;
        }

        return true;
    }*/

    bool CheckSAT(Vector2[] verticesA, Vector2[] verticesB)
    {
        Vector2[] axesA = new Vector2[2];
        Vector2[] axesB = new Vector2[2];

        GetAxes(verticesA, axesA,"First ");
        GetAxes(verticesB, axesB,"Second ");

        for (int i = 0; i < 2; i++)
        {
            ProjectShape(verticesA, axesA[i], out float minA, out float maxA);
            ProjectShape(verticesB, axesA[i], out float minB, out float maxB);

            if (!IntervalsOverlap(minA, maxA, minB, maxB))
                return false;
        }

        for (int i = 0; i < 2; i++)
        {
            ProjectShape(verticesA, axesB[i], out float minA, out float maxA);
            ProjectShape(verticesB, axesB[i], out float minB, out float maxB);

            if (!IntervalsOverlap(minA, maxA, minB, maxB))
                return false;
        }

        return true;
    }
}
