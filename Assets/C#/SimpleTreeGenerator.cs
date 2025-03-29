using UnityEngine;

public class SimpleTreeGenerator : MonoBehaviour
{
    public float trunkHeight = 5f; // 树干高度
    public float trunkRadius = 0.5f; // 树干半径
    public float crownRadius = 2f; // 树冠半径
    public Material trunkMaterial; // 树干材质（可选）
    public Material crownMaterial; // 树冠材质（可选）

    void Start()
    {
        GenerateTree();
    }

    void GenerateTree()
    {
        // 创建树干（使用圆柱体）
        GameObject trunk = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        trunk.name = "TreeTrunk";
        trunk.transform.localScale = new Vector3(trunkRadius, trunkHeight / 2, trunkRadius); // 设置树干的大小
        trunk.transform.position = new Vector3(0, trunkHeight / 2, 0); // 将树干放在地面上
        if (trunkMaterial != null)
        {
            trunk.GetComponent<Renderer>().material = trunkMaterial; // 应用树干材质
        }

        // 创建树冠（使用球体）
        GameObject crown = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        crown.name = "TreeCrown";
        crown.transform.localScale = new Vector3(crownRadius, crownRadius, crownRadius); // 设置树冠的大小
        crown.transform.position = new Vector3(0, trunkHeight + crownRadius / 2, 0); // 将树冠放在树干顶部
        if (crownMaterial != null)
        {
            crown.GetComponent<Renderer>().material = crownMaterial; // 应用树冠材质
        }

        // 将树干和树冠组合成一棵树
        GameObject tree = new GameObject("Tree");
        trunk.transform.parent = tree.transform;
        crown.transform.parent = tree.transform;
        tree.transform.parent = transform; // 将整棵树附加到当前GameObject
    }
}