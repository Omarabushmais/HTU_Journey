using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class AddMeshColliders : EditorWindow
{
    enum ColliderType { Mesh, Box, Sphere, Capsule }
    ColliderType selectedType = ColliderType.Box;
    Vector3 boxSize = new Vector3(1f, 1f, 1f);
    Vector3 boxCenter = Vector3.zero;
    float sphereRadius = 0.5f;
    float capsuleRadius = 0.5f;
    float capsuleHeight = 1f;

    [MenuItem("Tools/Add Collider Tool")]
    static void Open() => GetWindow<AddMeshColliders>("Add Collider Tool");

    void OnGUI()
    {
        GUILayout.Label("Collider Type", EditorStyles.boldLabel);
        selectedType = (ColliderType)EditorGUILayout.EnumPopup("Type", selectedType);

        GUILayout.Space(10);

        if (selectedType == ColliderType.Box)
        {
            boxSize = EditorGUILayout.Vector3Field("Size", boxSize);
            boxCenter = EditorGUILayout.Vector3Field("Center", boxCenter);
        }
        else if (selectedType == ColliderType.Sphere)
        {
            sphereRadius = EditorGUILayout.FloatField("Radius", sphereRadius);
        }
        else if (selectedType == ColliderType.Capsule)
        {
            capsuleRadius = EditorGUILayout.FloatField("Radius", capsuleRadius);
            capsuleHeight = EditorGUILayout.FloatField("Height", capsuleHeight);
        }
        else if (selectedType == ColliderType.Mesh)
        {
            GUILayout.Label("Auto-fits to mesh shape");
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Remove Existing Colliders"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                foreach (Collider c in obj.GetComponentsInChildren<Collider>(true))
                    DestroyImmediate(c);
            }
        }

        if (GUILayout.Button("Add Collider To Selected"))
        {
            foreach (GameObject obj in Selection.gameObjects)
            {
                // target the child with the mesh instead of root
                MeshFilter mf = obj.GetComponentInChildren<MeshFilter>(true);
                if (mf == null) continue;
                GameObject target = mf.gameObject;

                // remove old collider on that target
                Collider old = target.GetComponent<Collider>();
                if (old != null) DestroyImmediate(old);

                switch (selectedType)
                {
                    case ColliderType.Box:
                        BoxCollider bc = target.AddComponent<BoxCollider>();
                        bc.size = boxSize;
                        bc.center = boxCenter;
                        break;
                    case ColliderType.Sphere:
                        SphereCollider sc = target.AddComponent<SphereCollider>();
                        sc.radius = sphereRadius;
                        break;
                    case ColliderType.Capsule:
                        CapsuleCollider cc = target.AddComponent<CapsuleCollider>();
                        cc.radius = capsuleRadius;
                        cc.height = capsuleHeight;
                        break;
                    case ColliderType.Mesh:
                        MeshCollider mc = target.AddComponent<MeshCollider>();
                        mc.sharedMesh = mf.sharedMesh;
                        break;
                }
            }
        }
    }
}
#endif