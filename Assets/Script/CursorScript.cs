using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Vari�veis p�blicas que ser�o configuradas no Unity
    public GameObject destinyHud;
    public Texture2D cursorUpTexture;
    public Texture2D cursorDownTexture;
    public Texture2D cursorGrabTexture;
    public GameObject player;
    public List<int> rayIgnore;

    // Vari�veis privadas
    private Vector2 cursorHotSpot;

    private void Awake()
    {
        // Define o ponto de clique do cursor
        cursorHotSpot = new Vector2(0, 0);

        // Define o cursor padr�o
        Cursor.SetCursor(cursorUpTexture, cursorHotSpot, CursorMode.Auto);

        // Posiciona o destinyHud na posi��o do jogador
        destinyHud.transform.position = player.transform.position;

        // Ignora colis�es entre a camada "player" e a camada "prop"
        Physics.IgnoreLayerCollision(3, 9);
    }

    // Update � chamado uma vez por frame
    private void Update()
    {
        // Altera o cursor de acordo com o bot�o pressionado
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorDownTexture, cursorHotSpot, CursorMode.Auto);
        }
        else if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(cursorGrabTexture, cursorHotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorUpTexture, cursorHotSpot, CursorMode.Auto);
        }

        // Cria um raio a partir da camera at� a posi��o do mouse no plano 3D
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycast))
        {
            GameObject obj = raycast.collider.gameObject;
            // Posiciona o cursor na posi��o do raio
            transform.position = raycast.point;
            // Verifica se o bot�o esquerdo do mouse foi pressionado e n�o houve pressionamento de outros bot�es
            if (Input.GetMouseButton(0) && !(Input.GetMouseButton(1) || Input.GetMouseButton(2)))
            {
                // Verifica se a camada do objeto atingido pelo raio n�o est� na lista de camadas ignoradas
                if (!rayIgnore.Contains(obj.layer))
                {
                    // Posiciona o destinyHud na posi��o do raio
                    destinyHud.transform.position = raycast.point;
                    destinyHud.transform.Translate(Vector3.forward * -0.2f);
                    // Inicia a anima��o do destinyHud
                    destinyHud.GetComponent<Animator>().SetBool("IsMoving", true);
                }
            }
            else
            {
                // Para a anima��o do destinyHud
                destinyHud.GetComponent<Animator>().SetBool("IsMoving", false);
            }
        }
    }
}