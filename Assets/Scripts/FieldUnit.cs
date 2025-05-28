using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private PlayingField playingField;
    public TeamData teamData;
    public void InitializeUnit(TeamData teamData, PlayingField playingField)
    {
        this.playingField = playingField;

        SetType(teamData);
    }
    public void SetType(TeamData teamData)
    {
        this.teamData = teamData;

        spriteRenderer.color = teamData.color;
        gameObject.layer = teamData.layer;

        // TO DO: Method should change layerMask of unit collider
    }

}
