using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public enum GameState
    {
        wait,
        move
    }

    public GameState currentState = GameState.move;

    public int width = 8;
    public int height = 8;
    public float gemSpeed = 0.5f;

    public GameObject gemPrefab;
    public GameObject[,] allGems;
    public Gem[,] gems;

    private MatchFinder matchFinder;
    private ScoreManager scoreManager;

    void Start()
    {
        matchFinder = FindObjectOfType<MatchFinder>();
        scoreManager = FindObjectOfType<ScoreManager>();
        allGems = new GameObject[width, height];
        gems = new Gem[width, height];
        SetUp();
    }

    void SetUp()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject gem = Instantiate(gemPrefab, pos, Quaternion.identity);
                gem.transform.parent = transform;
                gem.name = "Gem(" + x + "," + y + ")";

                int gemType = Random.Range(0, 6);
                int maxIterations = 0;

                // Avoid creating matches at start
                while (CheckForMatches(x, y, (Gem.GemType)gemType) && maxIterations < 100)
                {
                    gemType = Random.Range(0, 6);
                    maxIterations++;
                }

                Gem gemComponent = gem.GetComponent<Gem>();
                gemComponent.column = x;
                gemComponent.row = y;
                gemComponent.gemType = (Gem.GemType)gemType;
                gemComponent.SetColor();

                allGems[x, y] = gem;
                gems[x, y] = gemComponent;
            }
        }
    }

    bool CheckForMatches(int column, int row, Gem.GemType type)
    {
        // Check left
        if (column > 1)
        {
            if (gems[column - 1, row] != null && gems[column - 2, row] != null)
            {
                if (gems[column - 1, row].gemType == type && gems[column - 2, row].gemType == type)
                {
                    return true;
                }
            }
        }

        // Check down
        if (row > 1)
        {
            if (gems[column, row - 1] != null && gems[column, row - 2] != null)
            {
                if (gems[column, row - 1].gemType == type && gems[column, row - 2].gemType == type)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void SwapGems(int column1, int row1, int column2, int row2)
    {
        if (allGems[column1, row1] != null && allGems[column2, row2] != null)
        {
            StartCoroutine(SwapGemsCoroutine(column1, row1, column2, row2));
        }
    }

    IEnumerator SwapGemsCoroutine(int column1, int row1, int column2, int row2)
    {
        currentState = GameState.wait;

        // Swap gem positions
        Gem gem1 = gems[column1, row1];
        Gem gem2 = gems[column2, row2];

        gem1.column = column2;
        gem1.row = row2;
        gem2.column = column1;
        gem2.row = row1;

        gems[column1, row1] = gem2;
        gems[column2, row2] = gem1;

        allGems[column1, row1] = gem2.gameObject;
        allGems[column2, row2] = gem1.gameObject;

        yield return new WaitForSeconds(0.5f);

        // Check if the swap created any matches
        matchFinder.FindAllMatches();

        if (gem1.isMatched || gem2.isMatched)
        {
            // Valid move - matches were created
            yield return StartCoroutine(ProcessMatches());
        }
        else
        {
            // Invalid move - swap back
            gem1.column = column1;
            gem1.row = row1;
            gem2.column = column2;
            gem2.row = row2;

            gems[column1, row1] = gem1;
            gems[column2, row2] = gem2;

            allGems[column1, row1] = gem1.gameObject;
            allGems[column2, row2] = gem2.gameObject;

            yield return new WaitForSeconds(0.5f);
        }

        currentState = GameState.move;
    }

    IEnumerator ProcessMatches()
    {
        while (matchFinder.currentMatches.Count > 0)
        {
            // Destroy matched gems
            yield return StartCoroutine(DestroyMatches());

            // Make gems fall
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FillBoard());

            // Check for new matches
            yield return new WaitForSeconds(0.5f);
            matchFinder.FindAllMatches();
        }

        yield return null;
    }

    IEnumerator DestroyMatches()
    {
        int matchCount = matchFinder.currentMatches.Count;

        if (scoreManager != null)
        {
            scoreManager.AddScore(matchCount * 10);
        }

        foreach (GameObject gem in matchFinder.currentMatches)
        {
            if (gem != null)
            {
                Gem gemComponent = gem.GetComponent<Gem>();
                if (gemComponent != null)
                {
                    allGems[gemComponent.column, gemComponent.row] = null;
                    gems[gemComponent.column, gemComponent.row] = null;
                }
                Destroy(gem);
            }
        }

        matchFinder.currentMatches.Clear();
        yield return null;
    }

    IEnumerator FillBoard()
    {
        // Move existing gems down
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    // Look for a gem above this position
                    for (int i = y + 1; i < height; i++)
                    {
                        if (allGems[x, i] != null)
                        {
                            // Move this gem down
                            allGems[x, i].GetComponent<Gem>().row = y;
                            allGems[x, y] = allGems[x, i];
                            gems[x, y] = gems[x, i];
                            allGems[x, i] = null;
                            gems[x, i] = null;
                            break;
                        }
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        // Fill empty spaces with new gems
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (allGems[x, y] == null)
                {
                    Vector2 pos = new Vector2(x, y + height);
                    GameObject gem = Instantiate(gemPrefab, pos, Quaternion.identity);
                    gem.transform.parent = transform;
                    gem.name = "Gem(" + x + "," + y + ")";

                    Gem gemComponent = gem.GetComponent<Gem>();
                    gemComponent.column = x;
                    gemComponent.row = y;
                    gemComponent.gemType = (Gem.GemType)Random.Range(0, 6);
                    gemComponent.SetColor();

                    allGems[x, y] = gem;
                    gems[x, y] = gemComponent;
                }
            }
        }

        yield return null;
    }
}
