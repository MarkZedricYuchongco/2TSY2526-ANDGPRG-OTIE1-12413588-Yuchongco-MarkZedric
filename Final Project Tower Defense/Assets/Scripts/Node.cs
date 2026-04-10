using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.green;
    public Color notPlaceableColor = Color.red;

    private GameObject tower;
    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        if (!BuildManager.Instance.CanBuild) return;

        if (tower != null)
            rend.material.color = notPlaceableColor;
        else
            rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    void OnMouseDown()
    {
        if (!BuildManager.Instance.CanBuild) return;

        if (tower != null)
        {
            Debug.Log("Node occupied!");
            return;
        }

        if (!BuildManager.Instance.HasMoney)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        TowerBlueprint blueprint = BuildManager.Instance.GetTowerToBuild();
        GameManager.Instance.gold -= blueprint.cost;

        tower = Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
    }
}