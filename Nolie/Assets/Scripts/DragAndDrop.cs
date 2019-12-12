using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool moveAllowed;
    private MoveableObject movedObject;


    void Update()
    {
        // On dit que si le compteur de doight est supérieur à 0 alors il se passe un quelquechose.
        if (Input.touchCount > 0)
        {
            DragAndDropFonction();
        }
    }


    void DragAndDropFonction()
    {
        // création d'une Variable Touch, égale à 0 si rien ne touche l'écran. = 1 si 1 doight touche l'écran
        // On veux check si le compteur est supérieur à 0
        // On crée alors une Touch variable pour répertorier le nombre de doight touchant l'écran
        Touch touch = Input.GetTouch(0);
        // ici on appelle la fonction de Unity pour trouver la position on l'on va déplacer notre object
        // Unity converti directement la pos des pixel en WorldPos lié à la caméra
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
        {
            Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);

            moveAllowed = true;
            movedObject = touchedCollider.GetComponent<MoveableObject>();
        }

        if (touch.phase == TouchPhase.Moved)
        {
            if (moveAllowed)
            {
                transform.position = new Vector2(touchPosition.x, touchPosition.y);
            }
        }

        if (touch.phase == TouchPhase.Ended)
        {
            moveAllowed = false;
        }
    }
}