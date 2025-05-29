using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    private const int FIELD_SIZE = 20;
    [SerializeField] private GameObject fieldUnitPrefab;
    [Header("Field data")]
    [SerializeField] private Color color1;
    [SerializeField] private LayerMask layerMask1;
    [SerializeField] private Material material1;
    [Space(15)]
    [SerializeField] private Color color2;
    [SerializeField] private LayerMask layerMask2;
    [SerializeField] private Material material2;

    private TeamData teamOne;
    private TeamData teamTwo;
    private void Awake()
    {
        teamOne = new TeamData
        {
            color = color1,
            layer = LayerMaskToLayerIndex(layerMask1),
            material = material1
        };

        teamTwo = new TeamData
        {
            color = color2,
            layer = LayerMaskToLayerIndex(layerMask2),
            material = material2
        };

        material1.color = color1;
        material2.color = color2;

        InitializeField();
    }
    private void InitializeField()
    {
        float unitWidth = fieldUnitPrefab.transform.localScale.x;
        float offset = unitWidth / 2;

        for (int x = FIELD_SIZE / 2 * -1; x < FIELD_SIZE / 2; x++)
        {
            for (int y = FIELD_SIZE / 2 * -1; y < FIELD_SIZE / 2; y++)
            {
                Vector2 unitPosition = new Vector2(x * unitWidth + offset, y * unitWidth + offset);

                var unitObject = Instantiate(fieldUnitPrefab, unitPosition, Quaternion.identity);
                var unitScript = unitObject.GetComponent<FieldUnit>();

                // Units on the left is on the team ONE, units on the right is the team TWO
                TeamData teamData = x < 0 ? teamOne : teamTwo;

                unitScript.InitializeUnit(teamData, this);

                unitObject.transform.SetParent(transform);
            }
        }
    }

    private int LayerMaskToLayerIndex(LayerMask mask)
    {
        int value = mask.value;

        if (value == 0 || (value & (value - 1)) != 0)
        {
            Debug.LogError("LayerMask must contain exactly one layer!");
            return 0;
        }

        return Mathf.RoundToInt(Mathf.Log(value, 2));
    }

}
