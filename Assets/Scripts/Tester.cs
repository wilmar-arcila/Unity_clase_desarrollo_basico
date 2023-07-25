using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    
    //[SerializeField] private GameObject testSubject;

    /* [SerializeField] private GameObject character;
    private InteractionEngine eventsPublisher;
    private void Start()
    {
        eventsPublisher = character.GetComponent<CharacterInstanciator>().getCharacter().GetComponent<InteractionEngine>();
        if (eventsPublisher != null) // Se suscribe a los respectivos eventos
        {
            eventsPublisher.CharacterChanged += OnCharacterChanged;
        }
    }
    private void OnDestroy()
    {
        if (eventsPublisher != null) // Cancela la suscripción a los eventos
        {
            eventsPublisher.CharacterChanged -= OnCharacterChanged;
        }
    }
    private void OnCharacterChanged()
    {
        Debug.Log("[Tester]Character Changed");
    } */
    
    /* private StatsController _script;
    private int _score, item1, item2, item3;
    private float timer;
    private bool l1, l2, l3;

    void Start()
    {
        _script = testSubject.GetComponent<StatsController>();
        _score = item1 = item2 = item3 = 10;
        l1 = l3 = true;
        l2 = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5f){
            Debug.Log("5s timer");
            timer = 0;
            _score += 5;
            item1 += 1;
            item2 += 2;
            item3 += 3;
            _script.updateScore(_score);
            _script.updateItems((item1, item2, item3));
            _script.updateLives((l1, l2, l3));
            _script.updatePowers((l1, l2, l3));
            l2 = l3;
            l1 = l2;
            l3 = false;
        }
    } */
}
