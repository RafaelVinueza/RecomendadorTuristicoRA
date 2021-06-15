using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    private Vector2 touchDeltaPosition;
    private GameObject botonCentrar;
    private CentrarUsuario centrarUsuario;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touchZero = Input.GetTouch(0);
            if(touchZero.phase == TouchPhase.Moved)
            {

                botonCentrar = GameObject.Find("ButtonCenter");

                if (botonCentrar != null)
                {
                    centrarUsuario = botonCentrar.GetComponent<CentrarUsuario>();
                    centrarUsuario.seguir = false;
                }

                touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Translate(-touchDeltaPosition.x * .05f, -touchDeltaPosition.y * .1f, 0);
            }
        }
    }
}
