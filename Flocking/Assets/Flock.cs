using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockManager myManager;
    public float speed;
    bool turning = false;

    // Start is called before the first frame update
    void Start()
    {   //Configuração de velocidade maxima e minima do cardume
        speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {    //Define a velocidade do cardume
        ApplyRules();
        transform.Translate(0, 0, Time.deltaTime * speed);

        Bounds b = new Bounds(myManager.transform.position, myManager.swinLimits * 2);

        RaycastHit hit = new RaycastHit();
        Vector3 direction = myManager.transform.position - transform.position;
        //Serve pra evitar que os peixes não passem por dentro do pilar (Em teoria)
        if (!b.Contains(transform.position))
        {
            turning = true;
            direction = myManager.transform.position - transform.position;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);
        }

        else
            turning = false;
        //Serve para que o cardume faça a rotação em torno do pilar
        if(turning)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (Random.Range(0, 100) < 10)
                speed = Random.Range(myManager.minSpeed, myManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                ApplyRules();
        }
        //Faz com que os peixes se movam
        transform.Translate(0, 0, Time.deltaTime * speed);
    }


    void ApplyRules()
    {   //Cria uma lista com todos os peixes setados acima
        GameObject[] gos;
        gos = myManager.allFish;
        
        //Cria o centro de rotação do cardume
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        //Velocidade do cardume
        float gSpeed = 0.01f;
        //Distancia entre os peixes 
        float nDistance;
        //Tamanho do cardume
        int groupSize = 0;
        
        //Faz o com que ciclo de rotação do cardume se repita
        foreach(GameObject go in gos)
        {   
            if(go != this.gameObject)
            {   //Cria o distanciamento entre os peixes do cardume
                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if(nDistance <= myManager.neighbourDistance)
                {   //Soma a posição do centro com o transform position do game object
                    vcentre += go.transform.position;
                    groupSize++;
                    //impede que os peixes saiam do ciclo de rotação em direção ao centro
                    if(nDistance < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    //PEga o componente do objecto com o flock
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }
            }
        }
        if(groupSize> 0)
        {   //Gera o centro do cardume em função do seu tamanho
            vcentre = vcentre / groupSize + (myManager.goalpos - this.transform.position);
            //velocidade de movimento do cardume
            speed = gSpeed / groupSize;
            //Gera o centro de movimentação do cardume
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        }
    }
    

}


