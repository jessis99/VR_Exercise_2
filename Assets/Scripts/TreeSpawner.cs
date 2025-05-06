using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    private float spawnInterval = 5f;
    private float spawnArea = 20f;
    public float minDistance = 3f;

    private float timer = 0f;

    private List<GameObject> trees = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTree();
            timer = 0f;
        }
    }

    void SpawnTree()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea, spawnArea),
            0,
            Random.Range(-spawnArea, spawnArea)
        );

        if(ensureMinDistance(randomPosition)){
            GameObject newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);
            trees.Add(newTree);
        }
        
    }

    bool ensureMinDistance(Vector3 randomPosition){             //liefert aktuell immer false, da irgendwann das Feld voll ist und die Baeume sich nciht ueberlagern duerfen
        foreach(GameObject tree in trees){
            if(Vector3.Distance(tree.transform.position, randomPosition) < minDistance){
                return false;
            }
        }
        return true;
    }

    public void baumEliminieren(GameObject tree){
        if (trees.Contains(tree))
        {
            trees.Remove(tree);
        }
    }
}
