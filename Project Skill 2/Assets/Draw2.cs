using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw2 : MonoBehaviour
{
    List<Vector3> points = new List<Vector3>();
    List<int> dd = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = Create();
    }
    public Mesh Create()
    {
        Mesh mesh = new Mesh();
        Draws(mesh);
        return mesh;
    }
    float ang = 60;
    float r = 4;
    private void Draws(Mesh mesh)
    {
        float middle = transform.parent.GetComponent<Collider>().bounds.size.x * 0.5f;
        points.Clear();
        points.Add(Vector3.zero);
        Vector3 startDir = (Quaternion.AngleAxis(ang * 0.5f, -Vector3.up) * Vector3.forward).normalized;
        int count = Mathf.CeilToInt(ang);
        float syAng = ang /count;
        for (int i = 0; i <= count; i++)
        {
            float rotate = syAng * i;
            Vector3 dir = Quaternion.AngleAxis(rotate, Vector3.up) * startDir;
            Vector3 point = dir * r;
            points.Add(point); 
        }
        mesh.vertices = points.ToArray();
        dd.Clear();
        for (int i = 0; i < count; i++)
        {
            dd.Add(0);
            dd.Add(i + 1);
            dd.Add(i + 2);
        }
        mesh.triangles = dd.ToArray();
        mesh.RecalculateNormals();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
