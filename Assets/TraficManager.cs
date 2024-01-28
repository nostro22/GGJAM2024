using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficManager : MonoBehaviour
{
    public GameObject upGreen;
    public GameObject upYellow;
    public GameObject upRed;

    public GameObject leftGreen;
    public GameObject leftYellow;
    public GameObject leftRed;

    public Animator cars;

    private void Start()
    {
        // Inicia la corrutina.
        StartCoroutine(ToggleObjects());
    }

    private void turnoff()
    {
        leftGreen.SetActive(false);
     leftYellow.SetActive(false);
     leftRed.SetActive(false);
    upGreen.SetActive(false);
     upYellow.SetActive(false);
     upRed.SetActive(false);

}
    IEnumerator ToggleObjects()
    {
        int tipo = 0;
       
            tipo = Random.Range(1, 7);
            // Espera un tiempo aleatorio entre 1 y 5 segundos.
            yield return new WaitForSeconds(Random.Range(1, 2));



            switch (tipo)
            {
                case 1:
                    turnoff();
                    leftRed.SetActive(true);
                    upYellow.SetActive(true);
                    yield return new WaitForSeconds(Random.Range(1, 2));
                    turnoff();
                    leftRed.SetActive(true);
                    upGreen.SetActive(true);
                    cars.SetTrigger("UP");
                    break;
                case 2:
                    turnoff();
                    leftRed.SetActive(true);
                    upYellow.SetActive(true);
                    yield return new WaitForSeconds(Random.Range(1, 2));
                    turnoff();
                leftRed.SetActive(true);
                upGreen.SetActive(true);
                    cars.SetTrigger("DOWN");
                    break;
                case 3:
                    turnoff();
                    leftYellow.SetActive(true);
                    upRed.SetActive(true);
                yield return new WaitForSeconds(Random.Range(1, 2));
                    turnoff();
                    upRed.SetActive(true);
                    leftGreen.SetActive(true);
                    cars.SetTrigger("LEFT");
                    break;
                case 4:
                    turnoff();
                    leftYellow.SetActive(true);
                    upRed.SetActive(true);
                yield return new WaitForSeconds(Random.Range(1, 2));
                    turnoff();
                    upRed.SetActive(true);
                    leftGreen.SetActive(true);
                    cars.SetTrigger("RIGHT");
                    break;
                case 5:
                    turnoff();
                    upYellow.SetActive(true);
                    leftRed.SetActive(true);
                    yield return new WaitForSeconds(Random.Range(1, 2));
                    turnoff();
                    leftRed.SetActive(true);
                    upRed.SetActive(true);
                break;
                case 6:
                    turnoff();
                    leftYellow.SetActive(true);
                    upRed.SetActive(true);
                    yield return new WaitForSeconds(Random.Range(1, 2));
                turnoff();
                    upRed.SetActive(true);
                    leftRed.SetActive(true);
                break;
               
            }

            //// Vuelve a llamar a esta corrutina, haciendo que sea recursiva.
            yield return StartCoroutine(ToggleObjects());
        
    }

}
