using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;



public class SeconWay : MonoBehaviour
{
    [SerializeField] GameObject fText;
    [SerializeField] GameObject opcoes;
    [SerializeField] Image img;
    [SerializeField] GameObject deadAna;
    [SerializeField] GameObject anaAlive;
    [SerializeField] GameObject goodLevel;
    [SerializeField] GameObject badLevel;
    [SerializeField] GameObject porta;
    [SerializeField] GameObject limit;
    [SerializeField] GameObject jukeboxBoa;
    [SerializeField] GameObject jukeboxMa;

    public Material skyboxMa;

    


    Animator portaAnim;


    bool fClicked;
    bool fVisible;
    bool jata;


    // Start is called before the first frame update
    void Start()
    {
        //skyboxMa = Resources.Load<Material>("Assets/Materials/Skybox Laranja.mat");
        portaAnim = porta.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fVisible)
        {
            if (Input.GetKey(KeyCode.F))
            {
                fClicked = true;
                fText.SetActive(false);
                fVisible = false;
            }
            }
        if (fClicked)
        {
            opcoes.SetActive(true);
            if (Input.GetKey(KeyCode.B))
            {
                StartCoroutine(DeadBicho());
                fClicked = false;
                limit.SetActive(false);
                jata = true;
               


            }
            else if (Input.GetKey(KeyCode.N))
            {
                portaAnim.SetBool("abre", true);
                opcoes.SetActive(false);
                fClicked = false;
                limit.SetActive(false);
                jata = true;
                



            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!jata)
            {
                fText.SetActive(true);
                fVisible = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fText.SetActive(false);
        fVisible = false;
    }

    IEnumerator DeadBicho()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
        yield return new WaitForSeconds(1.5f);
        deadAna.SetActive(true);
        anaAlive.SetActive(false);
        goodLevel.SetActive(false);
        badLevel.SetActive(true);
        opcoes.SetActive(false);
        RenderSettings.skybox = skyboxMa;
        jukeboxBoa.SetActive(false);
        jukeboxMa.SetActive(true);

        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
