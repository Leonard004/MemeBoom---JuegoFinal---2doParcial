using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public int NumCarta = 0;
    public Vector3 PosOriCarta;
    public Texture2D texturaAnverso;
    public Texture2D texturaReverso;

    public float tiempoDelay;
    public GameObject crearCartas;
    private bool mostrando;

    public GameObject interfazUsuario;

    private void Awake()
    {
        crearCartas = GameObject.Find("Scripts");
        interfazUsuario = GameObject.Find("Scripts");
    }

    private void Start()
    {
        EsconderCarta();
    }

    private void OnMouseDown()
    {
        if (!interfazUsuario.GetComponent<InterfazUsuario> ().menuMostrado)
        {
            MostrarCarta();
        }
    }

    public void AsignarTextura(Texture2D _textura)
    {
        texturaAnverso = _textura;
    }

    public void MostrarCarta()
    {
        if (!mostrando && crearCartas.GetComponent<CrearCarta>().sepuedeMostrar)
        {
            mostrando = true;
            GetComponent<MeshRenderer>().material.mainTexture = texturaAnverso;
            crearCartas.GetComponent<CrearCarta>().HacerClicks(this);
            //Invoke(nameof(EsconderCarta), tiempoDelay);
        }
    }

    public void EsconderCarta()
    {
        Invoke(nameof(Esconder), tiempoDelay);
        crearCartas.GetComponent<CrearCarta>().sepuedeMostrar = false;
    }

    void Esconder()
    {
        GetComponent<MeshRenderer>().material.mainTexture = texturaReverso;
        mostrando = false;
        crearCartas.GetComponent<CrearCarta>().sepuedeMostrar = true;
    }
}
