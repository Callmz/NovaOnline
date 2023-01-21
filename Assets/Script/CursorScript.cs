using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Variáveis públicas que serão configuradas no Unity
    public GameObject destinyHud;
    public Texture2D cursorUpTexture;
    public Texture2D cursorDownTexture;
    public Texture2D cursorGrabTexture;
    public GameObject player;
    public List<int> rayIgnore;

    // Variáveis privadas
    private Vector2 cursorHotSpot;

    private void Awake()
    {
        // Define o ponto de clique do cursor
        cursorHotSpot = new Vector2(0, 0);

        // Define o cursor padrão
        Cursor.SetCursor(cursorUpTexture, cursorHotSpot, CursorMode.Auto);

        // Posiciona o destinyHud na posição do jogador
        destinyHud.transform.position = player.transform.position;

        // Ignora colisões entre a camada "player" e a camada "prop"
        Physics.IgnoreLayerCollision(3, 9);
    }

    // Update é chamado uma vez por frame
    private void Update()
    {
        // Altera o cursor de acordo com o botão pressionado
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

        // Cria um raio a partir da camera até a posição do mouse no plano 3D
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycast))
        {
            GameObject obj = raycast.collider.gameObject;
            // Posiciona o cursor na posição do raio
            transform.position = raycast.point;
            // Verifica se o botão esquerdo do mouse foi pressionado e não houve pressionamento de outros botões
            if (Input.GetMouseButton(0) && !(Input.GetMouseButton(1) || Input.GetMouseButton(2)))
            {
                // Verifica se a camada do objeto atingido pelo raio não está na lista de camadas ignoradas
                if (!rayIgnore.Contains(obj.layer))
                {
                    // Posiciona o destinyHud na posição do raio
                    destinyHud.transform.position = raycast.point;
                    destinyHud.transform.Translate(Vector3.forward * -0.2f);
                    // Inicia a animação do destinyHud
                    destinyHud.GetComponent<Animator>().SetBool("IsMoving", true);
                }
            }
            else
            {
                // Para a animação do destinyHud
                destinyHud.GetComponent<Animator>().SetBool("IsMoving", false);
            }
        }
    }
}