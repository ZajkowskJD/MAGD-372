using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class CvAManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float forceMult;
    [SerializeField] private Rigidbody[] rbs;

    private bool started = false;
    private float duration = 0;

    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        if(sfx != null) duration = sfx.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else if (Input.GetKeyDown(KeyCode.C) && !started) StartCoroutine(_exampleCoroutine());
        else if (Input.GetKeyDown(KeyCode.A) && !started) _exampleAsync();
    }

    private IEnumerator _exampleCoroutine()
    {
        started = true;
        sfx.PlayOneShot(sfx.clip);
        anim.SetTrigger("start");
        yield return new WaitForSeconds(duration * 0.95f);
        foreach(Rigidbody rb in rbs)
        {
            Vector3 launchDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)) * forceMult;
            rb.AddForce(launchDir, ForceMode.Impulse);
        }
        Debug.Log("completed successfully");
    }

    private async void _exampleAsync()
    {
        started = true;
        var end = Time.time + (duration * 0.95f);
        sfx.PlayOneShot(sfx.clip);
        anim.SetTrigger("start");
        while (Time.time < end)
        {
            await Task.Yield();
        }
        foreach (Rigidbody rb in rbs)
        {
            Vector3 launchDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f)) * forceMult;
            rb.AddForce(launchDir, ForceMode.Impulse);
        }
        Debug.Log("completed successfully");
    }
}
