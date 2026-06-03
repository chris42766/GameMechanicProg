using UnityEngine;

public class TargetVertices : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject vertice0;
    public GameObject vertice1;
    public GameObject vertice2;
    public GameObject vertice3;


    public Vector2[] vertices = new Vector2[4];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vertices[0] = new Vector2(vertice0.transform.position.x, vertice0.transform.position.y);
        vertices[1] = new Vector2(vertice1.transform.position.x, vertice1.transform.position.y);
        vertices[2] = new Vector2(vertice2.transform.position.x, vertice2.transform.position.y);
        vertices[3] = new Vector2(vertice3.transform.position.x, vertice3.transform.position.y);
    }
}
