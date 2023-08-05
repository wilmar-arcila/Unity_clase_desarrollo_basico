using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInstanciator))]
public class CharacterStatsManager : MonoBehaviour
{
    /****************************/
    /*   VALORES POR DEFECTO    */
    /****************************/
    private const int defaultLives = 3;
    private const int defaultScore = 0;
    private const float defaultRespawnHigh = 6f;
    private const int defaultItem = 0;
    private const bool defaultPower = false;
    /*****************************/


    private float respawnHigh;
    private Vector3 respawnPoint;
    private int lives;
    private int score;
    private int[] items;
    private bool[] powers;

    private int scoreForItem1 = 1;
    private int scoreForItem2 = 2;
    private int scoreForItem3 = 3;
    private int scoreForPower = 10;

     //////////////////////////////////////////////
    /*         OBSERVER PATTERN (as Publisher)  */
    public event Action CharacterStatsChanged;
    public event Action CharacterGameOver;
    //////////////////////////////////////////////
    /*          OBSERVER PATTERN (as Observer)  */
    private GameObject character;
    private InteractionEngine characterInteractionPublisher;
    //////////////////////////////////////////////
    
    //////////////////////////////////////////////
    /*          SINGLETON PATTERN               */
    private static CharacterStatsManager Instance;
    private void Awake()
    {
        if(CharacterStatsManager.Instance == null){
            CharacterStatsManager.Instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this);
        }
    }
    public static CharacterStatsManager GetInstance(){
        return CharacterStatsManager.Instance;
    }
    //////////////////////////////////////////////

    //////////////////////////////////////////////
    /*    OBSERVER PATTERN (as Observer)        */
    public void InitializeCharacterStats(GameObject _character){
        Debug.Log("[CharacterStatsManager]InitializeCharacterStats");
        character = _character;

        if (character.TryGetComponent<InteractionEngine>(out characterInteractionPublisher))
        { // Se suscribe a los eventos en los que está interesado
            characterInteractionPublisher.CharacterItemsChanged += OnCharacterItemsChanged;
            characterInteractionPublisher.CharacterPowersChanged += OnCharacterPowersChanged;
            characterInteractionPublisher.CharacterLivesChanged += OnCharacterLivesChanged;
        }
        if(!GameManager.GetInstance().LockCharacterStats){
            ResetCharacterStats();
        }
    }
   
    private void OnDestroy()
    {
        if (characterInteractionPublisher != null) // Cancela la suscripción a los eventos
        {
            characterInteractionPublisher.CharacterItemsChanged -= OnCharacterItemsChanged;
            characterInteractionPublisher.CharacterPowersChanged -= OnCharacterPowersChanged;
            characterInteractionPublisher.CharacterLivesChanged -= OnCharacterLivesChanged;
        }
    }
    private void OnCharacterItemsChanged((int item1, int item2, int item3)deltaItems)
    {
        Debug.Log("[CharacterStatsManager]Items Changed: " + deltaItems);
        if(deltaItems.item1 != 0){
            ChangeItem1(deltaItems.item1);
        }
        if(deltaItems.item2 != 0){
            ChangeItem2(deltaItems.item2);
        }
        if(deltaItems.item3!= 0){
            ChangeItem3(deltaItems.item3);
        }
    }
    private void OnCharacterLivesChanged(int deltaLives)
    {
        Debug.Log("[CharacterStatsManager]Lives Changed: " + deltaLives);
        ChangeLives(deltaLives);
    }
    private void OnCharacterPowersChanged((int item1, int item2, int item3)deltaPowers)
    {
        Debug.Log("[CharacterStatsManager]Powers Changed: " + deltaPowers);
        if(deltaPowers.item1 != 0){
            ChangePower1(deltaPowers.item1 > 0);
        }
        if(deltaPowers.item2 != 0){
            ChangePower2(deltaPowers.item2 > 0);
        }
        if(deltaPowers.item3 != 0){
            ChangePower3(deltaPowers.item3 > 0);
        }
    }
    ///////////////////////////////////////////////
    
    private void ResetCharacterStats(){
        Debug.Log("[CharacterStatsManager]ResetCharacterStats");
        respawnHigh = defaultRespawnHigh;
        respawnPoint = new Vector3(0,defaultRespawnHigh,0);
        lives = defaultLives;
        score = defaultScore;
        items = new int[]{defaultItem,defaultItem,defaultItem};
        powers = new bool[]{defaultPower,defaultPower,defaultPower};
        CharacterStatsChanged?.Invoke();
    }

    public void SetScore(int _score){
        score = _score;
        CharacterStatsChanged?.Invoke();
    }
    public int GetScore(){
        return score;
    }
    private void RaiseScore(int _plusScore){
        score += _plusScore;
        CharacterStatsChanged?.Invoke();
    }

    public int GetLives(){
        return lives;
    }
    private void ChangeLives(int deltaL){
        lives += deltaL;
        CharacterStatsChanged?.Invoke();
        if(lives <= 0){
            Debug.Log("[CharacterStatsManager]GAME OVER");
            GameManager.GetInstance().LockCharacterStats = false;
            CharacterGameOver?.Invoke();
        }
    }

    public (int, int, int) GetItems(){
        return (items[0], items[1], items[2]);
    }
    public void SetItems((int item1, int item2, int item3)_items){
        items[0] = _items.item1;
        items[1] = _items.item2;
        items[2] = _items.item3;
        CharacterStatsChanged?.Invoke();
    }
    private void ChangeItem1(int _item){
        items[0] += _item;
        RaiseScore(scoreForItem1);
    }
    private void ChangeItem2(int _item){
        items[1] += _item;
        RaiseScore(scoreForItem2);
    }
    private void ChangeItem3(int _item){
        items[2] += _item;
        RaiseScore(scoreForItem3);
    }

    public (bool, bool, bool) GetPowers(){
        return (powers[0], powers[1], powers[2]);
    }
    public void SetPowers((bool item1, bool item2, bool item3)_powers){
        powers[0] = _powers.item1;
        powers[1] = _powers.item2;
        powers[2] = _powers.item3;
        CharacterStatsChanged?.Invoke();
    }
    private void ChangePower1(bool _power){
        powers[0] = _power;
        RaiseScore(scoreForPower);
    }
    private void ChangePower2(bool _power){
        powers[1] = _power;
        RaiseScore(scoreForPower);
    }
    private void ChangePower3(bool _power){
        powers[2] = _power;
        RaiseScore(scoreForPower);
    }

    public void SetRespawnPoint(Vector3 _respawnPoint){
        respawnPoint = new Vector3(_respawnPoint[0], respawnHigh, _respawnPoint[2]);
    }
    public Vector3 GetRespawnPoint(){
        return respawnPoint;
    }

    public void ResetRespawnPoint(){
        respawnPoint = new Vector3(0,defaultRespawnHigh,0);
    }

}