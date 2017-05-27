using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(NavPoint))]
public class NavPointInspector: Editor
{
    NavPoint navPoint;
    public static NavPoint navPointGainingNeighbors;

    void OnEnable()
    {
        navPoint = (NavPoint)target;
        EditorApplication.update += EditorAppUpdate;
        Tools.hidden = true;
    }

    void OnDisable()
    {
        Tools.hidden = false;
    }

    private void EditorAppUpdate()
    {
        // Get the selected element, and see if it's a nav point
        NavPoint selectedNavPoint = null;
        if (Selection.activeGameObject != null)
        {
            selectedNavPoint = Selection.activeGameObject.GetComponent<NavPoint>();
        }
        if (selectedNavPoint == null)
        {
            navPointGainingNeighbors = null;
        }
        // Is there a node currently looking for neighbors, and is it different from what was selected
        else if (navPointGainingNeighbors != null && selectedNavPoint != navPointGainingNeighbors)
        {
            // Is it already in the list?
            if (navPointGainingNeighbors != null)
            {
                if (navPointGainingNeighbors.Neighbors != null)
                {
                    var foundNeighbor = navPointGainingNeighbors.Neighbors.FirstOrDefault(nb => nb.NeighborPoint == selectedNavPoint);
                    if (foundNeighbor == null)
                    {
                        // Add it to the list
                        List<NavNeighbor> neighborList = new List<NavNeighbor>(navPointGainingNeighbors.Neighbors);
                        neighborList.Add(new NavNeighbor { NeighborPoint = selectedNavPoint, TravelType = TravelTypes.Walk });
                        navPointGainingNeighbors.Neighbors = neighborList.ToArray();

                        // Also add this one to the neighbor
                    }
                    else
                    {
                        // Remove this one only
                        navPointGainingNeighbors.Neighbors = navPointGainingNeighbors.Neighbors.Where(np => np.NeighborPoint != selectedNavPoint)
                            .ToArray();
                    }
                }

                // Re-select the node that's gaining neighbors
                Selection.activeGameObject = navPointGainingNeighbors.gameObject;
                Selection.activeInstanceID = navPointGainingNeighbors.gameObject.GetInstanceID();
            }
        }
    }

    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        serializedObject.Update();
        //EditorGUIUtility.LookLikeInspector();
        SerializedProperty tps = serializedObject.FindProperty("Neighbors");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tps, true);
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
        if (navPointGainingNeighbors == null)
        {
            if (GUILayout.Button("Add/Remove Neighbors"))
            {
                navPointGainingNeighbors = navPoint;
                Tools.current = Tool.None;
            }
            if (GUILayout.Button("Clear Neighbors"))
            {
                navPoint.Neighbors = new NavNeighbor[0];
            }
        }
        else
        {
            if (GUILayout.Button("-- FINISHED --"))
            {
                navPointGainingNeighbors = null;
                Tools.current = Tool.Move;
            }
        }
        //EditorGUIUtility.LookLikeControls();
    }

    // Draw scene stuff
    public void OnSceneGUI()
    {
        // The handles get in the way of selecting neighbors
        if (navPointGainingNeighbors == null)
        {
            navPoint.transform.position = Handles.PositionHandle(navPoint.transform.position, navPoint.transform.rotation);
        } else
        {
            Handles.color = Color.red;
            Handles.SphereCap(0, navPoint.transform.position, Quaternion.identity, 0.5f);
        }

        // Indicate the connected neighbors
        if (navPoint.Neighbors != null) {
            Handles.color = Color.green;
            foreach (var neighbor in navPoint.Neighbors) {
                if (neighbor.NeighborPoint != null)
                {
                    Handles.SphereCap(0, neighbor.NeighborPoint.transform.position, Quaternion.identity, 0.5f);
                }
            }
        }
    }
}
