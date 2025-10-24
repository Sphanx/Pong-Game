using UnityEngine;

public class Gem : MonoBehaviour
{
    public enum GemType
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
        Orange
    }

    public GemType gemType;
    public int column;
    public int row;
    public bool isMatched = false;

    private Board board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    private float swipeAngle = 0;
    private float swipeResist = 1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        board = FindObjectOfType<Board>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Update position to match grid position
        if (Mathf.Abs(column - transform.position.x) > 0.1f)
        {
            tempPosition = new Vector2(column, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
        }
        else
        {
            tempPosition = new Vector2(column, transform.position.y);
            transform.position = tempPosition;
        }

        if (Mathf.Abs(row - transform.position.y) > 0.1f)
        {
            tempPosition = new Vector2(transform.position.x, row);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, row);
            transform.position = tempPosition;
        }
    }

    private void OnMouseDown()
    {
        if (board.currentState == Board.GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == Board.GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y,
                                  finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;

        if (Vector3.Distance(firstTouchPosition, finalTouchPosition) > swipeResist)
        {
            MovePieces();
        }
    }

    void MovePieces()
    {
        // Right swipe
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            board.SwapGems(column, row, column + 1, row);
        }
        // Up swipe
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            board.SwapGems(column, row, column, row + 1);
        }
        // Left swipe
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            board.SwapGems(column, row, column - 1, row);
        }
        // Down swipe
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            board.SwapGems(column, row, column, row - 1);
        }
    }

    public void SetColor()
    {
        if (spriteRenderer != null)
        {
            switch (gemType)
            {
                case GemType.Red:
                    spriteRenderer.color = Color.red;
                    break;
                case GemType.Blue:
                    spriteRenderer.color = Color.blue;
                    break;
                case GemType.Green:
                    spriteRenderer.color = Color.green;
                    break;
                case GemType.Yellow:
                    spriteRenderer.color = Color.yellow;
                    break;
                case GemType.Purple:
                    spriteRenderer.color = new Color(0.5f, 0f, 0.5f);
                    break;
                case GemType.Orange:
                    spriteRenderer.color = new Color(1f, 0.5f, 0f);
                    break;
            }
        }
    }
}
