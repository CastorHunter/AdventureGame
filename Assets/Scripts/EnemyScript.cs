using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class EnemyScript : MonoBehaviour
// {
//     public bool followingPlayer;
//     public float speed;
//     private Rigidbody2D myRigidbody;
//     private Vector3 change;
//     public Transform weapon;
//     public float offset;
//     void Start()
//     {
//     }
//     void Update()
//     {
//     }
//     void MoveCharacter()
//     {
//     }
//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log(change);
//             MoveCharacter();
//         }
//     }
// }
// int xDir = 0;
// int yDir = 0;
// xDir = target.position.x > transform.position.x ? 1 : -1;
// yDir = target.position.y > transform.position.y ? 1 : -1;
namespace Completed
{
    // Le mot clé abstract vous permet de créer des classes et des membres de classes qui sont incomplets et qui doivent être implémentés dans les classes filles.
    public abstract class EnemyScript : MonoBehaviour
    {
        public float moveTime = 0.1f;           // Temps que cela prend de déplacer l'objet, en seconde.
        public LayerMask blockingLayer;         // Couche sur laquelle la collision doit être vérifiée.
        
        
        private BoxCollider2D boxCollider;      // Le composant BoxCollider2D attaché à cet objet.
        private Rigidbody2D rb2D;               // Le composant Rigidbody2D attaché à cet objet.
        private float inverseMoveTime;          // Utilisé pour rendre les mouvements plus efficaces.
        
        
        // Protected, les fonctions virtuelles peuvent être surchargées par les classes filles.
        protected virtual void Start ()
        {
            // Récupère une référence sur le composant BoxCollider2D de l'objet
            boxCollider = GetComponent <BoxCollider2D> ();
            
            // Récupère une référence sur le composant Rigidbody2D de l'objet
            rb2D = GetComponent <Rigidbody2D> ();
            
            // En stockant l'inverse du temps de mouvement, nous pouvons l'utiliser dans une multiplication au lieu d'une division, c'est plus efficace.
            inverseMoveTime = 1f / moveTime;
        }
        
        
        // La fonction Move retourne true si elle peut faire le déplacement ou false sinon. 
        // La fonction Move prend les paramètres x et y pour la direction et un RaycastHit2D pour vérifier la collision.
        protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
        {
            // Stocke la position de départ depuis laquelle on se déplace, selon la position actuelle de l'objet.
            Vector2 start = transform.position;
            
            // Calcul la position de fin suivant les paramètres de direction.
            Vector2 end = start + new Vector2 (xDir, yDir);
            
            // Désactive le boxCollider afin que le lancer de rayon ne touche pas la boîte englobante pour cet objet.
            boxCollider.enabled = false;
            
            // Envoie une ligne du point de départ vers la fin en vérifiant les collisions de la couche blockingLayer.
            hit = Physics2D.Linecast (start, end, blockingLayer);
            
            // Réactive le boxCollider après le lancer de rayon.
            boxCollider.enabled = true;
            
            // Vérifie si quelque chose a été touché.
            if(hit.transform == null)
            {
                // Si rien n'a été touché, démarre la coroutine SmoothMovement en passant la fin comme destination.
                StartCoroutine (SmoothMovement (end));
                
                // Retourne true pour indiquer que le mouvement a été un succès.
                return true;
            }
            
            // Si quelque chose a été touché, retourne false, le mouvement n'est pas possible.
            return false;
        }
        
        
        // Coroutine pour déplacer les éléments d'une case à l'autre, en prenant la destination comme paramètre.
        protected IEnumerator SmoothMovement (Vector3 end)
        {
            // Calcul la distance restante à effectuer en se basant sur le carré de la différence entre la position actuelle et la destination. 
            // Le carré est utilisé à la place de la magnitude, car c'est moins coûteux en calcul.
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            
            // Tant que la distance est plus grande qu'un très petit nombre (Epsilon, presque zéro) :
            while(sqrRemainingDistance > float.Epsilon)
            {
                // Trouve une nouvelle position proportionnelle à la destination et basée sur moveTime.
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
                
                // Appelle MovePosition sur le Rigidbody2D attaché et le déplace à la position calculée.
                rb2D.MovePosition (newPostion);
                
                // Recalcule la distance restante après le mouvement.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                
                // Retourne et boucle jusqu'à avoir un sqrRemainingDistance assez proche de zéro pour terminer la fonction.
                yield return null;
            }
        }
        
        
        // Le mot clé virtual signifie que la fonction AttemptMove peut être surchargée avec le mot clé override et en héritant de la classe.
        // La fonction AttemptMove prend un paramètre générique T pour spécifier le type de composant avec lequel l'unité va interagir si elle est bloquée (le joueur pour les ennemies, les murs pour le joueur).
        protected virtual void AttemptMove <T> (int xDir, int yDir)
            where T : Component
        {
            // La variable hit conservera l'objet bloquant le rayon lorsque la fonction Move est appelée.
            RaycastHit2D hit;
            
            // Définit canMove à true si la fonction Move a réussi, false sinon.
            bool canMove = Move (xDir, yDir, out hit);
            
            // Vérifie si le lancer de rayon n'a rien touché.
            if(hit.transform == null)
                // Si rien n'a été touché, retourne et n'exécute rien de plus.
                return;
            
            // Récupère une référence sur le composant de type T attaché à l'objet qui a été touché.
            T hitComponent = hit.transform.GetComponent <T> ();
            
            // Si la variable canMove est false et que hitComponent n'est pas null alors MovingObject est bloqué et a touché quelque chose avec lequel il peut interagir.
            if(!canMove && hitComponent != null)
                
                // Appelle la fonction OnCantMove et lui passe hitComponent comme paramètre.
                OnCantMove (hitComponent);
        }
        
        
        // Le mot clé abstract indique que l'objet en cours de modification a une implémentation incomplète.
        // La fonction OnCantMove va être surchargée par les fonctions de la classe fille.
        protected abstract void OnCantMove <T> (T component)
            where T : Component;
    }
}
namespace Completed
{
    // Enemy hérite de MovingObject, notre classe de base pour les objets mobiles. La classe Player hérite aussi de cette classe.
    public class Enemy : EnemyScript
    {
        public int playerDamage;                            // Le nombre de points à enlever au joueur lors d'une attaque.
        
        
        private Animator animator;                          // Variable de type Animator pour garder une référence vers le composant Animator de l'ennemi.
        private Transform target;                           // Déplacement vers lequel on essaie d'aller à chaque tour. 
        private bool skipMove;                              // Booléen pour déterminer si l'ennemi doit passer son tour ou se déplacer.
        
        
        // La fonction Start surcharge la fonction virtuelle Start de la classe de base.
        protected override void Start ()
        {
            // Enregistre cet ennemi à l'instance de GameManager en l'ajoutant à la liste des ennemis. 
            // Cela permet au GameManager de faire les commandes de déplacement.
            GameManager.instance.AddEnemyToList (this);
            
            // Récupère et stocke une référence sur le composant Animator.
            animator = GetComponent<Animator> ();
            
            // Trouve le GameObject Player en utilisant son tag et stocke une référence de son composant Transform.
            target = GameObject.FindGameObjectWithTag ("Player").transform;
            
            // Appelle la fonction Start de la classe de base.
            base.Start ();
        }
        
        
        // Surcharge la fonction AttemptMove de la classe MovingObject pour intégrer la particularité de Enemy à sauter des tours.
        // Voir les commentaires de MovingObject pour plus d'informations sur le fonctionnement de la fonction AttemptMove.
        protected override void AttemptMove <T> (int xDir, int yDir)
        {
            // Vérifie si skipMove est à true. Dans ce cas, le mettre à false et sauter le tour.
            if(skipMove)
            {
                skipMove = false;
                return;
                
            }
            
            // Appelle la fonction AttemptMove de la classe de base.
            base.AttemptMove <T> (xDir, yDir);
            
            // Maintenant que l'ennemi s'est déplacé, définit skipMove à true pour sauter le prochain tour.
            skipMove = true;
        }
        
        
        // La fonction MoveEnemy est appelée par le GameManger chaque tour pour indiquer à chaque ennemi d'essayer de se déplacer vers le joueur.
        public void MoveEnemy ()
        {
            // Déclare les variables pour les directions X et Y. Leur valeur varie entre -1 et 1.
            // Ces valeurs nous permettent de choisir parmi les directions cardinales : haut, bas, gauche et droite.
            int xDir = 0;
            int yDir = 0;
            
            // Si la différence de position est proche de zéro (Epsilon) faire ce qui suit :
            if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
                
                // Si la coordonnée Y de la cible (joueur) est plus grande que la coordonnée Y de cet ennemi, alors la direction en Y est 1 (pour se déplacer vers le haut). Sinon, c'est -1 (pour se déplacer vers le bas).
                yDir = target.position.y > transform.position.y ? 1 : -1;
            
            // Si la différence de position est proche de zéro (Epsilon) faire ce qui suit :
            else
                // Vérifie si la coordonnée X de la cible et plus grande que celle de cet ennemi. Dans ce cas, définit la direction en X à 1 (pour se déplacer vers la droite), sinon c'est -1 (pour se déplacer vers la gauche).
                xDir = target.position.x > transform.position.x ? 1 : -1;
            
            // Appelle la fonction AttemptMove en lui passant le paramètre générique Player, car Enemy se déplace et espère rencontrer un Player
            AttemptMove <Player> (xDir, yDir);
        }
        
        
        // La fonction OnCantMove est appelée si Enemy essaie de se déplacer vers un espace occupé par un Player. Surcharge la fonction OnCantMove de la classe MovingObject 
        //et prend un paramètre générique T par lequel nous passons le composant que nous souhaitons rencontrer, dans ce cas, le joueur.
        protected override void OnCantMove <T> (T component)
        {
            // Déclare hitPlayer et le définit à la valeur du composant rencontré.
            Player hitPlayer = component as Player;
            
            // Appelle la fonction LoseFood en lui passant playerDamage, le nombre de points de nourriture à enlever.
            hitPlayer.LoseFood (playerDamage);
            
            // Définit le déclencheur de l'attaque de l'animator pour démarrer l'animation d'attaque.
            animator.SetTrigger ("enemyAttack");

        }
    }
}