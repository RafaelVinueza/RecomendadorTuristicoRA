using UnityEngine;

public class Variables : MonoBehaviour
{
    //Renato
    //PointOfInterest[] ubicaciones = {
    //        new PointOfInterest(0.797570589763192, -77.7343844224615,"Chifa Fortuna'z","8:00 - 16:00","Restaurante"),
    //        new PointOfInterest(0.7982035297014667, -77.73527491576561,"Multiplaza Tulcán","8:00 - 16:00","Shopping"),
    //        new PointOfInterest(0.7977744178895334, -77.7349959660559,"KFC","8:00 - 16:00","Restaurante"),
    //        new PointOfInterest(0.7982154992163683, -77.733701997274,"Inglesia Cristo Rey","8:00 - 16:00","Iglesia"),
    //        new PointOfInterest(0.7968136580403848, -77.73528022715637,"ECU","8:00 - 16:00","Policia"),
    //        new PointOfInterest(0.7971073543001528, -77.73258732274684,"Asilo Sara Espíndola","8:00 - 16:00",""),  //mas de 300m
    //        new PointOfInterest(0.8015651959185869, -77.72888289520522,"Parque del 8","8:00 - 16:00","Parque"),
    //        new PointOfInterest(0.7955523099510798, -77.73274231648688,"UTPL Tulcán","8:00 - 16:00","UniEducativa"),
    //        new PointOfInterest(0.8015987187972252, -77.72720321720541,"Colegio Vicente Fierro","8:00 - 16:00","UniEducativa"),
    //        new PointOfInterest(0.7984349063279026, -77.72924912823457,"Centro de Salud","8:00 - 16:00","Hospital"),
    //        new PointOfInterest(0.7979326528216261, -77.7351488521371,"Seminario mayor","8:00 - 16:00","Iglesia")
    //};


    //mios
    private PointOfInterest[] ubicaciones = {
            new PointOfInterest(-0.12881892216479307, -78.48514214490213, "Punto general", "0:00 - 14:00", "General"),
            new PointOfInterest(-0.12901950898813322, -78.48726269159879, "KFC", "0:00 - 12:00", "Restaurante"),
            new PointOfInterest(-0.12987513203891415, -78.48722782385948, "San Francisco", "0:00 - 16:00", "Iglesia"),
            new PointOfInterest(-0.13063419498339018, -78.48504450430167, "El arbolito", "0:00 - 15:00", "Parque"),
            new PointOfInterest(-0.12880493251384292, -78.48486211424304, "McDonalds", "0:00 - 24:00", "Restaurante"),
            new PointOfInterest(-0.11937160243827653, -78.48624337664937, "Nombre", "0:00 - 24:00", "Gasolineria"),
            new PointOfInterest(-0.12945685636551782, -78.48246990396505, "Nombre", "0:00 - 24:00", "Museo"),  //mas de 300m
            new PointOfInterest(-0.13339925527698535, -78.48669255818707, "Nombre", "0:00 - 24:00", "Hotel"),
            new PointOfInterest(-0.12792185450610694, -78.48895910189235, "Nombre", "0:00 - 24:00", "Cine"),
            new PointOfInterest(-0.12792185450610694, -78.48895910189235, "Nombre", "0:00 - 24:00", "Deporte"),
            new PointOfInterest(-0.1259208276554583, -78.48637067637105, "Nombre", "0:00 - 24:00", "UniEducativa")
    };

    public Variables()
    {
    }

    public PointOfInterest[] Ubicaciones { get => ubicaciones; set => ubicaciones = value; }
}
