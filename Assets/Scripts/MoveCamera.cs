using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    public Button closeMap;

    private Vector2 touchDeltaPosition;
    private GameObject botonCentrar;
    private CentrarUsuario centrarUsuario;
    private Touch touchZero;

    private void Awake()
    {
        closeMap.onClick.AddListener(GameObject.Find("Controller").GetComponent<ChangeScene>().LoadScene);
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            touchZero = Input.GetTouch(0);
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
