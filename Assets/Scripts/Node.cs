using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    private Renderer render;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        render = GetComponent<Renderer>();
        startColor = render.material.color;
    }

    public Vector3 GetNodePosition ()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("TEST");
            return;
        }

        buildManager.BuildTurretOn(this);

    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasMoney)
        {
            render.material.color = hoverColor;
        }
        else
        {
            render.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        render.material.color = startColor;
    }
}
