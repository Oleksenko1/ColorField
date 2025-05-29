using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUnit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider2D;

    private PlayingField playingField;
    public TeamData teamData;
    public void InitializeUnit(TeamData teamData, PlayingField playingField)
    {
        this.playingField = playingField;

        spriteRenderer.material = teamData.material;

        SetType(teamData);
    }
    public void SetType(TeamData teamData)
    {
        this.teamData = teamData;

        gameObject.layer = teamData.layer;

        boxCollider2D.includeLayers = 1 << teamData.layer;

        // TO DO: Method should change layerMask of unit collider
    }

}
