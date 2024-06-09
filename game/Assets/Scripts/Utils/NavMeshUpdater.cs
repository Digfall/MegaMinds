using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdater : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public GameObject navMeshModifierObject;

    void Start()
    {
        if (navMeshSurface == null)
        {
            navMeshSurface = GetComponent<NavMeshSurface>();
        }

        StartCoroutine(CheckForScreenSizeChange());
    }

    IEnumerator CheckForScreenSizeChange()
    {
        Vector2 lastScreenSize = new Vector2(Screen.width, Screen.height);

        while (true)
        {
            if (Screen.width != lastScreenSize.x || Screen.height != lastScreenSize.y)
            {
                UpdateNavMesh();
                lastScreenSize = new Vector2(Screen.width, Screen.height);
            }

            yield return null;
        }
    }

    public void UpdateNavMesh()
    {
        Bounds newBounds = CalculateBounds(navMeshModifierObject);
        navMeshSurface.size = newBounds.size;
        navMeshSurface.center = newBounds.center;

        navMeshSurface.BuildNavMesh();
    }

    private Bounds CalculateBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds();
        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }
        return bounds;
    }
}