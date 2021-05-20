using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public Material colorCarta;
    public int idCarta = 0;
    public Vector3 posicionOriginal;
  
    void OnMouseDown()
    {
        print(idCarta.ToString());
    }
    public void PonerColor(Texture2D textura)
    {
        GetComponent<MeshRenderer>().material.mainTexture = textura;
        //colorCarta = color_;
    }
}
