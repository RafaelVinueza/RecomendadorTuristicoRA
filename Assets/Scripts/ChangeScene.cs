using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Validations validations;
    string errorDatosServicio;
    string sceneActual;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "SceneForm")
        {
            validations = GameObject.Find("ButtonSubmit").GetComponent<Validations>();
            errorDatosServicio = PlayerPrefs.GetString("errorDatosServicio");
            if(errorDatosServicio != null && errorDatosServicio != "")
            {
                validations.textError.text = errorDatosServicio;
                PlayerPrefs.SetString("errorDatosServicio", "");
                errorDatosServicio = null;
            }
        }
    }

    public void LoadScene()
    {
        sceneActual = SceneManager.GetActiveScene().name;
        switch (sceneActual)
        {
            case "SceneForm":
                if (validations.validate())
                {
                    SceneManager.LoadScene("SceneLoading");
                }
                break;
            case "Scene1":
                SceneManager.LoadScene("Scene2");
                break;
            case "Scene2":
                SceneManager.LoadScene("Scene1");
                break;
        }

        //SceneManager.LoadScene(sceneName);
    }

}
