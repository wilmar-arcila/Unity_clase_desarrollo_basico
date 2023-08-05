using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsController : MonoBehaviour
{
    private TMP_Text    _score     = null;
    private TMP_Text[]  _items     = null;
    private Image[]     _powers    = null;
    private Image[]     _lives     = null;
    private Image[]     _NO_lives  = null;    

    private CharacterStatsManager manager;

    //////////////////////////////////////////////
    /*          OBSERVER PATTERN (as Observer)  */
    private void Start()
    {
        manager = CharacterStatsManager.GetInstance();
        if (manager != null) // Se suscribe a los respectivos eventos
        {
            manager.CharacterStatsChanged += OnCharacterStatsChanged;
        }
        InitializePanel();
    }
    private void OnDestroy()
    {
        if (manager != null) // Cancela la suscripción a los eventos
        {
            manager.CharacterStatsChanged -= OnCharacterStatsChanged;
        }
    }
    private void OnCharacterStatsChanged()
    {
        UpdateLives(manager.GetLives());
        UpdatePowers(manager.GetPowers());
        UpdateItems(manager.GetItems());
        UpdateScore(manager.GetScore());
    }
    //////////////////////////////////////////////

    private void InitializePanel()
    {
        _items    = new TMP_Text[3];
        _powers   = new Image[3];
        _lives    = new Image[3];
        _NO_lives = new Image[3];

        _lives[0]     = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        _lives[1]     = transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>();
        _lives[2]     = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>();
        _NO_lives[0]  = transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        _NO_lives[1]  = transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<Image>();
        _NO_lives[2]  = transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<Image>();
        _score        = transform.GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>();
        _items[0]     = transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        _items[1]     = transform.GetChild(2).GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>();
        _items[2]     = transform.GetChild(2).GetChild(2).GetChild(0).gameObject.GetComponent<TMP_Text>();
        _powers[0]    = transform.GetChild(3).GetChild(0).gameObject.GetComponent<Image>();
        _powers[1]    = transform.GetChild(3).GetChild(1).gameObject.GetComponent<Image>();
        _powers[2]    = transform.GetChild(3).GetChild(2).gameObject.GetComponent<Image>();

        UpdateLives(manager.GetLives());
        UpdatePowers(manager.GetPowers());
        UpdateItems(manager.GetItems());
        UpdateScore(manager.GetScore());
    }

    private void UpdateLives(int lives){
        _lives[0].enabled = lives>0;
        _NO_lives[0].enabled = !_lives[0].enabled;
        _lives[1].enabled = lives>1;
        _NO_lives[1].enabled = !_lives[1].enabled;
        _lives[2].enabled = lives>2;
        _NO_lives[2].enabled = !_lives[2].enabled;
    }
    /* public void updateLives((bool live1, bool live2, bool live3)lives){
        _lives[0].enabled = lives.live1?true:false;
        _NO_lives[0].enabled = !_lives[0].enabled;
        _lives[1].enabled = lives.live2?true:false;
        _NO_lives[1].enabled = !_lives[1].enabled;
        _lives[2].enabled = lives.live3?true:false;
        _NO_lives[2].enabled = !_lives[2].enabled;
    } */

    private void UpdateScore(int score){
        if(score < 0){
            _score.text = "00000";
        }
        else if(score > 99999){
            _score.text = "99999";
        }
        else{
            _score.text = score.ToString("00000");
        }
    }

    private void UpdateItems((int item1, int item2, int item3)items){
        if(items.item1 < 0){
            _items[0].text = "00";
        }
        else if(items.item1 > 99){
            _items[0].text = "99";
        }
        else{
            _items[0].text = items.item1.ToString("00");
        }

        if(items.item2 < 0){
            _items[1].text = "00";
        }
        else if(items.item2 > 99){
            _items[1].text = "99";
        }
        else{
            _items[1].text = items.item2.ToString("00");
        }

        if(items.item3 < 0){
            _items[2].text = "00";
        }
        else if(items.item3 > 99){
            _items[2].text = "99";
        }
        else{
            _items[2].text = items.item3.ToString("00");
        }
    }

    private void UpdatePowers((bool item1, bool item2, bool item3)powers){
        Color tempColor;

        tempColor = _powers[0].color;
        tempColor.r = powers.item1?1f:0f;
        tempColor.g = powers.item1?1f:0f;
        tempColor.b = powers.item1?1f:0f;
        tempColor.a = powers.item1?1f:0.2392f;
        _powers[0].color = tempColor;

        tempColor = _powers[1].color;
        tempColor.r = powers.item2?1f:0f;
        tempColor.g = powers.item2?1f:0f;
        tempColor.b = powers.item2?1f:0f;
        tempColor.a = powers.item2?1f:0.2392f;
        _powers[1].color = tempColor;

        tempColor = _powers[2].color;
        tempColor.r = powers.item3?1f:0f;
        tempColor.g = powers.item3?1f:0f;
        tempColor.b = powers.item3?1f:0f;
        tempColor.a = powers.item3?1f:0.2392f;
        _powers[2].color = tempColor;
    }
}
