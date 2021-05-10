using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    //Cria slots para guardar as informações
    public GameObject fishPrefab;
    public int numFish= 20;
   //Cria a lista de peixes
    public GameObject[] allFish; 
    //Cria os limites do menu
    public Vector3 swinLimits = new Vector3(5, 5, 5);
    public Vector3 goalpos;

      //Menu para limitar a velocidade e rotação 
    [Header("Configurações do Cardume")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {    //Coloca os peixes na lista
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {   //Fazer os peixes nadarem em uma direção 
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x), 
                                                                Random.Range(-swinLimits.y, swinLimits.y), 
                                                                Random.Range(-swinLimits.z, swinLimits.z));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
        //Faz com que o goalpos seja a posição do objeto
        goalpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Seta os limites do goalpos
        goalpos = this.transform.position + new Vector3(Random.Range(-swinLimits.x, swinLimits.x),
                                                                Random.Range(-swinLimits.y, swinLimits.y),
                                                                Random.Range(-swinLimits.z, swinLimits.z));

    }
}
