using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<MeshFilter>().mesh = CreateSectorMesh();
    }
    private Mesh CreateSectorMesh()
    {
        Mesh mesh = new Mesh();
        DrawS(mesh);
        return mesh;
    }
    List<Vector3> points = new List<Vector3>();
    float ang = 60;
    float r = 4;
    List<int> ds = new List<int>();
    public void DrawS(Mesh mesh)
    {
        float middle = transform.parent.GetComponent<Collider>().bounds.size.z / 2;
        points.Clear();
        points.Add(Vector3.zero);

        Vector3 startDir = (Quaternion.AngleAxis(ang * 0.5f, -Vector3.up)*Vector3.forward).normalized;
        int x = Mathf.CeilToInt(ang);
        float preAngle = ang / x;
        for (int i = 0; i <= x; i++)
        {
            float rotateAngle = preAngle * i;
            Vector3 dir = Quaternion.AngleAxis(rotateAngle, Vector3.up) * startDir;
            Vector3 point =  dir * r;
            point.z += middle;
            points.Add(point);
        }
        mesh.vertices = points.ToArray();
        ds.Clear();
        for (int i = 0; i < x; i++)
        {
            ds.Add(0);
            ds.Add(i + 1);
            ds.Add(i + 2);
        }
        mesh.triangles = ds.ToArray();
        mesh.RecalculateNormals();
    }
}
