using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in the scene!");
            return;
        }

        instance = this;
    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }


    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.money < turretToBuild.cost)
        {
            Debug.Log("You cannot buy this turret!");
            return;
        }

        PlayerStats.money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetNodePosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetNodePosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret build! Money left: " + PlayerStats.money);
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
