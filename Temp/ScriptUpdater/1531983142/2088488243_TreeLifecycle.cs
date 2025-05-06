using UnityEngine;

public class TreeLifecycle : MonoBehaviour
{
    private float growthSpeed = 0.2f;
    private float startSize = 0.2f;
    private float maxSize = 3f;
    private float lifetime = 15f; // Sekunden bis zum Verschwinden
    private float currentLifetime = 0f;
    public TreeSpawner treeSpawner;
    private float fadeDuration = 2f;
    private Material material;
    private Color originalColor;

    void Start(){
        transform.localScale = new Vector3(startSize, startSize, startSize);

        if (treeSpawner == null)
        {
            treeSpawner = FindObjectOfType<TreeSpawner>(); // Sucht beliebigen Spawner in der Szene
        }

        material = GetComponent<Renderer>().material;
        originalColor = material.color;

    }

    void Update()
    {
        // Wachsen
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }

        currentLifetime += Time.deltaTime;

        // Baum verschwindet nach Ablauf der Lebenszeit
        if (currentLifetime >= lifetime)
        {
            FadeOut();
            treeSpawner.baumEliminieren(this.gameObject);
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        // Komplett unsichtbar setzen & zerstören
        material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Destroy(gameObject);
    }
}
