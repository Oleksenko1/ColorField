using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    private const int FIELD_SIZE = 20;
    [SerializeField] private GameObject fieldUnitPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private LayerMask borderLayer;
    [Header("Stats")]
    [SerializeField] private float ballSpeed;
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

        // Spawning units
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

        // Spawning balls 
        SpawnBall(teamOne, unitWidth, 1);
        SpawnBall(teamTwo, unitWidth, -1);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="teamData"></param>
    /// <param name="unitWidth"></param>
    /// <param name="side">-1 is left side, 1 is right side</param>
    private void SpawnBall(TeamData teamData, float unitWidth, int side)
    {
        float xPos = Random.Range(FIELD_SIZE / 2 * side * unitWidth - side, side * unitWidth);
        float yPos = Random.Range(FIELD_SIZE / 2 * -1 * unitWidth, FIELD_SIZE / 2 - 1 * unitWidth);

        Vector3 spawnPos = new Vector2(xPos, yPos);

        var ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        ball.layer = teamData.layer;

        int borderLayerIndex = LayerMaskToLayerIndex(borderLayer);
        Physics2D.IgnoreLayerCollision(teamData.layer, borderLayerIndex, false);

        var rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.one * side * ballSpeed, ForceMode2D.Impulse);

        ball.GetComponent<SpriteRenderer>().material = teamData.material;
    }

}
