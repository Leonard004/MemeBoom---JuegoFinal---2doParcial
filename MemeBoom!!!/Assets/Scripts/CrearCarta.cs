using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrearCarta : MonoBehaviour
{
    public GameObject CartaPrefad;
    public int ancho;
    public Transform cartaParent;
    private readonly List<GameObject> cartas = new List<GameObject>();

    public Material[] meteriales;
    public Texture2D[] textura;

    public int contadorClicks;
    public Text texContaIntentos;

    public Carta cartaMostrada;
    public bool sepuedeMostrar = true;

    public InterfazUsuario interfazUsuario;
    public int numParejasEncontradas;

    void Start()
    {
        Crear();
    }

    public void Reiniciar()
    {
        /*
        ancho = 0;
        cartas.Clear();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Carta");

        for (int i = 0; i < gameObjects.Length; i++)
        {
            DestroyObject(gameObjects[i]);
            //DestroyImmediate(gameObjects[i]);
        }

        contadorClicks = 0;
        texContaIntentos.text = "Intentos: ";
        cartaMostrada = null;
        sepuedeMostrar = true;
        numParejasEncontradas = 0;
        

        Crear();
        */

        SceneManager.LoadScene("SampleScene");

    }


    public void Crear()
    {

        ancho = interfazUsuario.dificultad;
        int cont = 0;
        for (int i = 0; i < ancho; i++)
        {
            for (int x = 0; x < ancho; x++)
            {
                float factor = 9.0f / ancho;
                Vector3 posicionTemp = new Vector3(x * factor, 0, i * factor);

                GameObject cartaTemp = Instantiate(CartaPrefad, new Vector3(x, 0, i), Quaternion.Euler(new Vector3(0, 180, 0)));
                cartaTemp.transform.localScale *= factor;

                cartas.Add(cartaTemp);

                cartaTemp.GetComponent<Carta>().PosOriCarta = new Vector3(x, 0, i);
                //cartaTemp.GetComponent<Carta>().NumCarta = cont;

                cartaTemp.transform.parent = cartaParent;

                cont++;
            }
        }
        AsignarTexturas();
        Barajear();
        interfazUsuario.ReiniciarCronometro();
        interfazUsuario.ActivarCronometro();
    }

    void AsignarTexturas()
    {
        int[] arrayTemp = new int[textura.Length];

        for (int i = 0; i <= textura.Length - 1; i++)
        {
            arrayTemp[i] = i;
        }

        for (int t = 0; t < arrayTemp.Length; t++)
        {
            int tmp = arrayTemp[t];
            int r = Random.Range(t, arrayTemp.Length);
            arrayTemp[t] = arrayTemp[r];
            arrayTemp[r] = tmp;
        }
        int[] arrayDefinitivo = new int[ancho * ancho];
        //int[] arrayDefinitivo = new int[(ancho * ancho)/2];

        for (int i = 0; i < arrayDefinitivo.Length; i++)
        {
            arrayDefinitivo[i] = arrayTemp[i];
        }

        for (int i = 0; i < arrayDefinitivo.Length; i++)
        {
            cartas[i].GetComponent<Carta>().AsignarTextura(textura[(arrayDefinitivo[i / 2])]);
            cartas[i].GetComponent<Carta>().NumCarta = i / 2;
            print(i / 2);
        }
    }

    void Barajear()
    {
        int aleatorio;

        for (int i = 0; i < cartas.Count; i++)
        {
            aleatorio = Random.Range(i, cartas.Count);
            cartas[i].transform.position = cartas[aleatorio].transform.position;
            cartas[aleatorio].transform.position = cartas[i].GetComponent<Carta>().PosOriCarta;

            cartas[i].GetComponent<Carta>().PosOriCarta = cartas[i].transform.position;
            cartas[aleatorio].GetComponent<Carta>().PosOriCarta = cartas[aleatorio].transform.position;

        }
    }

    public void HacerClicks(Carta _carta)
    {
        if (cartaMostrada == null)
        {
            cartaMostrada = _carta;
        }
        else
        {
            contadorClicks++;
            ActualizarUI();
            if (CompararCartas(_carta.gameObject, cartaMostrada.gameObject))
            {
                print("EnHorabuena! Has Encontrado una Pareja");
                numParejasEncontradas++;

                if (numParejasEncontradas == cartas.Count / 2)
                {
                    print("EnHorabuena! Has Encontrado Todas las Pareja");
                    interfazUsuario.MostrarMenuGanador();
                }
            }
            else
            {
                _carta.EsconderCarta();
                cartaMostrada.EsconderCarta();
            }
            cartaMostrada = null;
        }
    }

    public bool CompararCartas(GameObject carta1, GameObject carta2)
    {
        bool resultado;

        //if (carta1.GetComponent<MeshRenderer>().material.mainTexture ==
        //carta2.GetComponent<MeshRenderer>().material.mainTexture)
        if (carta1.GetComponent<Carta>().NumCarta ==
            carta2.GetComponent<Carta>().NumCarta)
        {
            resultado = true;
        }
        else
        {
            resultado = false;
        }
        return resultado;
    }

    public void ActualizarUI()
    {
        texContaIntentos.text = "Intentos: " + contadorClicks;
    }
}
