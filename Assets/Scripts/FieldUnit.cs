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

        SetType(teamData);
    }
    public void SetType(TeamData teamData)
    {
        this.teamData = teamData;

        gameObject.layer = teamData.layer;

        spriteRenderer.material = teamData.material;

        // TO DO: Method should change layerMask of unit collider
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            playingField.ToggleTeam(this);
        }
    }

}
