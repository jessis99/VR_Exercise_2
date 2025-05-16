using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TreeLifecycle : MonoBehaviour
{
    private float growthSpeed = 0.2f;
    private float startSize = 0.2f;
    private float maxSize = 3f;
    private float lifetime = 15f; // Sekunden bis zum Verschwinden
    private float currentLifetime = 0f;
    public TreeSpawner treeSpawner;
    private float fallDuration = 2f;
    private Vector3 startPos;
    private Vector3 endPos;
    private float timer = 0f;
    private Renderer treeRenderer;
    private Color initialColor;
    public float sinkAmount = 0.5f; //Einsinktiefe
    public Vector3 fallDirection = Vector3.forward; // Fallrichtung
    private bool dyingStarted = false; // damit coroutine nur einmal ausgefuehrt wird
    public bool birdChoseThisTree; // falls zwei Baeume gleich gross sind
    private bool reachedByBird = false;
    public GameObject bird;

    BirdPathF birdPath;

    void Start(){
        transform.localScale = new Vector3(startSize, startSize, startSize);

        if (treeSpawner == null)
        {
            treeSpawner = FindObjectOfType<TreeSpawner>(); // Sucht beliebigen Spawner in der Szene
        }

        if (bird == null)
        {
            bird = GameObject.Find("Sparrow"); // Sucht Vogel
        }

        if (birdPath == null)
        {
            birdPath = FindObjectOfType<BirdPathF>();
        }

        treeRenderer = GetComponent<Renderer>();
        initialColor = treeRenderer.material.color;

        // Setze den Shader auf Transparent
        treeRenderer.material.SetFloat("_Mode", 2);
        treeRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        treeRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        treeRenderer.material.SetInt("_ZWrite", 0);
        treeRenderer.material.DisableKeyword("_ALPHATEST_ON");
        treeRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        treeRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        treeRenderer.material.renderQueue = 3000;
    }

    void Update()
    {
        // Wachsen
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;
        }

        currentLifetime += Time.deltaTime;

        float timeLeft = lifetime - currentLifetime;

        if(Vector3.Distance(bird.transform.position, transform.position) < 6){
            reachedByBird = true;
        }

        // The trees start pulsating and blinking red in the last 5 seconds of their life
        if (timeLeft <= 5f && !dyingStarted)
        {
            // Blinking red
            float blink = Mathf.PingPong(Time.time * 5f, 1f); // faster blinking
            Color blinkColor = Color.Lerp(initialColor, Color.red, blink);
            treeRenderer.material.color = blinkColor;

            // Pulsating
            float pulse = 1f + Mathf.Sin(Time.time * 10f) * 0.05f; // 5% pulse
            transform.localScale = new Vector3(pulse, pulse, pulse) * Mathf.Min(maxSize, transform.localScale.x + 0.001f);
        }

        // Baum verschwindet nach Ablauf der Lebenszeit
        if (currentLifetime >= lifetime && !dyingStarted)
        {
            StartCoroutine(Die());
            dyingStarted = true;
            treeSpawner.baumEliminieren(this.gameObject);
        }
    }

    public void treeSelected(){
        Color selectColor = Color.Lerp(initialColor, Color.blue, 1);
        treeRenderer.material.color = selectColor;
    }

    public void treeUnselected(){
        treeRenderer.material.color = initialColor;
    }

    private IEnumerator Die(){
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(fallDirection * 90f) * startRot;
        startPos = transform.position;
        endPos = startPos - new Vector3(0, sinkAmount, 0); //versinken

        while(timer < fallDuration){
            timer = timer + Time.deltaTime;
            float t = timer / fallDuration;

            //fallen und halb im Boden versinken
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            //langsam transparent
            float alpha = Mathf.Clamp01((5f-timer) / fallDuration);
            Color transparentColor = initialColor;
            transparentColor.a = alpha;
            treeRenderer.material.color = transparentColor;
            yield return null;
        }
        transform.rotation = endRot;
        transform.position = endPos;

        // Vogel muss neuen Baum suchen
        if(birdChoseThisTree)
        {
            birdPath.birdHasTree = false;
        }

        Destroy(gameObject);

        if(reachedByBird){
            Points.increaseScore();
        }else{
            Points.decreaseScore();
        }
    }  
}
