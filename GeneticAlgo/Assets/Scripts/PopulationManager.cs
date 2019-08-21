using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    public GameObject personPref;

    public static float elapsed=0;

    public float RangeX;
    public float RangeY;

    public Transform populationContainer;

    int trialTime = 10;

    int generation = 1;

    const float minSize = 0.1f;
    const float maxSize = 0.3f;

    [SerializeField]
    int PopulationSize = 8;

    List<GameObject> population = new List<GameObject>();

    GUIStyle guiStyle = new GUIStyle();

    private void Start()
    {
        CreatePopuplation();
    }

    void CreatePopuplation()
    {
        for (int i = 0; i < PopulationSize; i++)
        {
            var go = GetNewBeing();
            go.GetComponent<DNA>().r = Random.Range(0F, 1F);
            go.GetComponent<DNA>().g = Random.Range(0F, 1F);
            go.GetComponent<DNA>().b = Random.Range(0F, 1F);
            go.GetComponent<DNA>().size = Random.Range(minSize, maxSize);

            population.Add(go);
        }
    }

    GameObject GetNewBeing()
    {
        Vector3 pos = new Vector3(Random.Range(-RangeX, RangeX), Random.Range(-RangeY, RangeY), 0);

        GameObject go = Instantiate(personPref, pos, Quaternion.identity, populationContainer);

        return go;
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Select Bighest to dullest ", guiStyle);
        GUI.Label(new Rect(10, 10 +55, 100, 20), "Generation " + generation, guiStyle);
        GUI.Label(new Rect(10, 10 + 55 + 55, 100, 20), "Trial Time " + (int)elapsed, guiStyle);
    }

    void breedNewPopulation()
    {      
        //get rid of the unfit 
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();

        population.Clear();

        //Breed Upper Half of the sorted list
        for (int i = (int) (sortedList.Count / 2.0f) - 1   ; i < sortedList.Count-1 ; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i+1], sortedList[i]));
        }

        //Destory all parents and previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= trialTime)
        {
            breedNewPopulation();
            elapsed = 0;
        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        var offspring = GetNewBeing();

        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        //Mix the Genes or go for the mutation
        if (Random.Range(0, 1000) > 5)
        {
            offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            offspring.GetComponent<DNA>().size = Random.Range(0, 10) < 5 ? dna1.size : dna2.size;
        }
        else
        {
            offspring.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            offspring.GetComponent<DNA>().size = Random.Range(minSize, maxSize);
        }

        return offspring;
    }
}
