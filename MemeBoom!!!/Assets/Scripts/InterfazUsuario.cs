using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InterfazUsuario : MonoBehaviour
{
    public GameObject menu;
    public GameObject menuGanador;

    public Text textMenuGanador;
    public Slider sliverDif;
    public Text textDificultad;

    public bool menuMostrado;
    public bool menuMostradoGabador;
    public int dificultad;

    public int segCronometro;
    public Text cronometro;
    TimeSpan tiempo;

    private void Start()
    {
        CambiarDificultad();
    }
    public void MostrarMenu()
    {
        menu.SetActive(true);
        menuMostrado = true;
    }

    public void EsconderMenu()
    {
        menu.SetActive(false);
        menuMostrado = false;
    }

    public void MostrarMenuGanador()
    {
        menu.SetActive(true);
        menuMostradoGabador = true;
    }

    public void EsconderMostrarMenuGanador()
    {
        menu.SetActive(false);
        menuMostradoGabador = false;
    }

    public void CambiarDificultad()
    {
        dificultad = (int)sliverDif.value * 2;
        textDificultad.text = "Dificultad: " + dificultad;
    }

    public void ActivarCronometro()
    {
        ActualizarCronometro();
    }

    public void ReiniciarCronometro()
    {
        segCronometro = 0;
        CancelInvoke("ActualizarCronometro");
    }

    public void ActualizarCronometro()
    {
        segCronometro++;

        tiempo = new TimeSpan(0, 0, segCronometro);
        cronometro.text = tiempo.ToString();
        Invoke("ActualizarCronometro", 1.0f);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
