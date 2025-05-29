using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBallsCount : MonoBehaviour
{
    [SerializeField] private PlayingField playingField;
    [SerializeField] private TextMeshProUGUI teamOneTxt;
    [SerializeField] private TextMeshProUGUI teamTwoTxt;
    int teamOneAmount;
    int teamTwoAmount;

    void Start()
    {
        teamOneAmount = teamTwoAmount = PlayingField.FIELD_SIZE * PlayingField.FIELD_SIZE / 2;

        playingField.OnFieldChanged += UpdateCount;

        SetTextColor();

        UpdateText();
    }
    private void UpdateCount(int teamNumber)
    {
        switch (teamNumber)
        {
            // If team number one lost a field unit
            case 1:
                teamOneAmount--;
                teamTwoAmount++;
                break;
            // If team number two lost a field unit
            case 2:
                teamOneAmount++;
                teamTwoAmount--;
                break;
        }

        UpdateText();
    }
    private void UpdateText()
    {
        teamOneTxt.SetText(teamOneAmount.ToString());
        teamTwoTxt.SetText(teamTwoAmount.ToString());
    }

    private void SetTextColor()
    {
        List<TeamData> teamList = playingField.GetTeams();

        teamOneTxt.color = teamList[0].material.color;
        teamTwoTxt.color = teamList[1].material.color;
    }
}
