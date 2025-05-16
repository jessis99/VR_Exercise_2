using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab;
    private float spawnInterval = 5f;
    [SerializeField] float minSpawnInterval = 4f;
    [SerializeField] float maxSpawnInterval = 6f;
    private float spawnArea = 50f;
    public float minDistance = 3f;
    private float timer = 0f;

    private List<GameObject> trees = new List<GameObject>();

    [SerializeField] GameObject firstTree;

    void Start()
    {
        trees.Add(firstTree);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTree();
            timer = 0f;
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
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

    bool ensureMinDistance(Vector3 randomPosition){            
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

    public Vector3 BiggestTreePosition()
    {
        trees[0].GetComponent<TreeLifecycle>().birdChoseThisTree = true;
        return trees[0].transform.position;
    }
}
