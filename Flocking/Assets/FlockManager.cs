using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    //Cria slots para guardar as informações
    public GameObject fishPrefab;
    public int numFish= 20;
    public GameObject[] allFish; 
    public Vector3 swinLimits = new Vector3(5, 5, 5);

      //Menu para limitar a velocidade 
    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {    //Coloca os peixes na lista
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {//Fazer os peixes nadarem em uma direção 
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), 
                                                                Random.Range(-swinLimits.y, swinLimits.y), 
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
