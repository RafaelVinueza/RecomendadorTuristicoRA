using UnityEngine;

public class CentrarUsuario : MonoBehaviour
{
    public bool seguir = false;
 
    public void seguirUsuario()
    {
        if (!seguir)
            seguir = true;
        else
            seguir = false;
    }

}
