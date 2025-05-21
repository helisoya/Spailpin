using UnityEngine;

public class TutorialPuzzleSFX : MonoBehaviour
{
    // Création de la fonction permettant de jouer un event en particulier
    public void PlayCarillon(int Bell)
    {
        //FMODEvents(script attaché à AudioManager).instance(créer une instance).NomEvent, Position))
        AudioManager.instance.PlayOneShotParameter(FMODEvents.instance.Carillon, this.transform.position, "Carillon", Bell);
    }
}
