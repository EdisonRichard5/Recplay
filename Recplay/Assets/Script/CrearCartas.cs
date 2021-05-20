using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCartas : MonoBehaviour
{
    public GameObject CartaPrefab;
    public int ancho;
    public int alto;
    public Transform Cartitas;
    private List<GameObject> cartas = new List<GameObject>();

    public Material[] materiales;
    public Texture2D[] texturas;
    
    public void Crear()
    {
        int cont = 0;
        for(int i = 0; i < ancho; i++)
        {
            for(int x = 0; x < alto; x++)
            {
                GameObject cartaTemp = Instantiate(CartaPrefab, new Vector3(x, i, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
                cartas.Add(cartaTemp);
                cartaTemp.GetComponent<Carta>().posicionOriginal = new Vector3(x, i, 0);
                cartaTemp.GetComponent<Carta>().idCarta = cont;

                cartaTemp.transform.parent=Cartitas;
                cont=cont+1;
            }
        }
        AsignarTexturas();
        Barajar();
    }
    void AsignarTexturas()
    {
        for (int i = 0; i < cartas.Count; i++)
        {
            cartas[i].GetComponent<Carta>().PonerColor(texturas[(i) / 2]);
        }
    }
    void Barajar()
    {
        int aleatorio;
        for (int i = 0; i < cartas.Count; i++)
        {
            aleatorio = Random.Range(i, cartas.Count);
            cartas[i].transform.position = cartas[aleatorio].transform.position;
            cartas[aleatorio].transform.position = cartas[i].GetComponent<Carta>().posicionOriginal;
            cartas[i].GetComponent<Carta>().posicionOriginal = cartas[i].transform.position;
            cartas[aleatorio].GetComponent<Carta>().posicionOriginal = cartas[aleatorio].transform.position;
        }
    }
    void Start()
    {
        Crear();
    }
}
