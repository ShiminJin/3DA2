using UnityEngine;

public class Road3DGenerator : MonoBehaviour
{
    public float roadLength = 100f; // 公路长度
    public float roadWidth = 10f;   // 公路宽度
    public float roadThickness = 1f; // 公路厚度（高度）
    public Color roadColor = Color.gray; // 公路颜色
    public Color laneColor = Color.white; // 车道线颜色

    void Start()
    {
        // 创建公路主体
        GameObject road = new GameObject("Road");
        MeshFilter meshFilter = road.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = road.AddComponent<MeshRenderer>();
        Mesh mesh = CreateRoadMesh();

        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = roadColor;

        // 创建车道线（中间线）
        CreateLaneLine(road.transform, laneColor, 0.5f);

        // 创建两侧路沿石（可选）
        CreateCurb(road.transform, -roadWidth / 2 - 0.5f, Color.black);
        CreateCurb(road.transform, roadWidth / 2 + 0.5f, Color.black);
    }

    // 创建3D公路网格（长方体）
    Mesh CreateRoadMesh()
    {
        Mesh mesh = new Mesh();

        // 定义顶点（8个顶点形成长方体）
        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-roadLength / 2, 0, -roadWidth / 2); // 顶面左前
        vertices[1] = new Vector3(roadLength / 2, 0, -roadWidth / 2);  // 顶面右前
        vertices[2] = new Vector3(-roadLength / 2, 0, roadWidth / 2);  // 顶面左后
        vertices[3] = new Vector3(roadLength / 2, 0, roadWidth / 2);   // 顶面右后
        vertices[4] = new Vector3(-roadLength / 2, -roadThickness, -roadWidth / 2); // 底面左前
        vertices[5] = new Vector3(roadLength / 2, -roadThickness, -roadWidth / 2);  // 底面右前
        vertices[6] = new Vector3(-roadLength / 2, -roadThickness, roadWidth / 2);  // 底面左后
        vertices[7] = new Vector3(roadLength / 2, -roadThickness, roadWidth / 2);   // 底面右后

        // 定义三角形索引（共36个索引，形成12个三角形）
        int[] triangles = new int[36];
        // 顶面（y=0）
        triangles[0] = 0; triangles[1] = 1; triangles[2] = 2;
        triangles[3] = 1; triangles[4] = 3; triangles[5] = 2;
        // 底面（y=-roadThickness）
        triangles[6] = 4; triangles[7] = 5; triangles[8] = 6;
        triangles[9] = 5; triangles[10] = 7; triangles[11] = 6;
        // 前面（z=-roadWidth/2）
        triangles[12] = 0; triangles[13] = 4; triangles[14] = 1;
        triangles[15] = 1; triangles[16] = 4; triangles[17] = 5;
        // 后面（z=roadWidth/2）
        triangles[18] = 2; triangles[19] = 6; triangles[20] = 3;
        triangles[21] = 3; triangles[22] = 6; triangles[23] = 7;
        // 左侧（x=-roadLength/2）
        triangles[24] = 0; triangles[25] = 2; triangles[26] = 4;
        triangles[27] = 2; triangles[28] = 4; triangles[29] = 6;
        // 右侧（x=roadLength/2）
        triangles[30] = 1; triangles[31] = 3; triangles[32] = 5;
        triangles[33] = 3; triangles[34] = 5; triangles[35] = 7;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        return mesh;
    }

    // 创建车道线（中间线）
    void CreateLaneLine(Transform parent, Color color, float width)
    {
        GameObject line = new GameObject("LaneLine");
        line.transform.parent = parent;

        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Lines/Colored"));
        lr.startColor = lr.endColor = color;
        lr.startWidth = lr.endWidth = width;
        lr.positionCount = 2;

        // 车道线位于公路顶部表面
        lr.SetPosition(0, new Vector3(-roadLength / 2, 0.1f, 0));
        lr.SetPosition(1, new Vector3(roadLength / 2, 0.1f, 0));
    }

    // 创建路沿石（长方体）
    void CreateCurb(Transform parent, float yPosition, Color color)
    {
        GameObject curb = new GameObject("Curb");
        curb.transform.parent = parent;
        curb.transform.localPosition = new Vector3(0, 0.1f, yPosition); // 位于公路顶部

        MeshFilter meshFilter = curb.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = curb.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();

        // 路沿石尺寸：长=roadLength，宽=0.5，高=0.5
        float curbWidth = 0.5f;
        float curbHeight = 0.5f;

        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-roadLength / 2, 0, -curbWidth / 2);
        vertices[1] = new Vector3(roadLength / 2, 0, -curbWidth / 2);
        vertices[2] = new Vector3(-roadLength / 2, 0, curbWidth / 2);
        vertices[3] = new Vector3(roadLength / 2, 0, curbWidth / 2);
        vertices[4] = new Vector3(-roadLength / 2, curbHeight, -curbWidth / 2);
        vertices[5] = new Vector3(roadLength / 2, curbHeight, -curbWidth / 2);
        vertices[6] = new Vector3(-roadLength / 2, curbHeight, curbWidth / 2);
        vertices[7] = new Vector3(roadLength / 2, curbHeight, curbWidth / 2);

        // 定义路沿石的三角形索引
        int[] triangles = new int[36];
        // 顶面
        triangles[0] = 0; triangles[1] = 1; triangles[2] = 2;
        triangles[3] = 1; triangles[4] = 3; triangles[5] = 2;
        // 底面
        triangles[6] = 4; triangles[7] = 5; triangles[8] = 6;
        triangles[9] = 5; triangles[10] = 7; triangles[11] = 6;
        // 前面
        triangles[12] = 0; triangles[13] = 4; triangles[14] = 1;
        triangles[15] = 1; triangles[16] = 4; triangles[17] = 5;
        // 后面
        triangles[18] = 2; triangles[19] = 6; triangles[20] = 3;
        triangles[21] = 3; triangles[22] = 6; triangles[23] = 7;
        // 左侧
        triangles[24] = 0; triangles[25] = 2; triangles[26] = 4;
        triangles[27] = 2; triangles[28] = 4; triangles[29] = 6;
        // 右侧
        triangles[30] = 1; triangles[31] = 3; triangles[32] = 5;
        triangles[33] = 3; triangles[34] = 5; triangles[35] = 7;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;

        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material.color = color;
    }
}