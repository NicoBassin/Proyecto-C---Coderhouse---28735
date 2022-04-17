using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTwoCast : MonoBehaviour
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform spellParent;
    private float timeDelay = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerAttack>().OnAttackTwo += SpellTwo;
    }

    private void SpellTwo(){
        StartCoroutine(Cast());
    }

    IEnumerator Cast(){
        yield return new WaitForSeconds(timeDelay);
        
    }
}
