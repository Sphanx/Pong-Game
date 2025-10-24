using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        currentMatches.Clear();

        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                GameObject currentGem = board.allGems[x, y];

                if (currentGem != null)
                {
                    Gem currentGemComponent = currentGem.GetComponent<Gem>();

                    // Check horizontal matches
                    if (x > 0 && x < board.width - 1)
                    {
                        GameObject leftGem = board.allGems[x - 1, y];
                        GameObject rightGem = board.allGems[x + 1, y];

                        if (leftGem != null && rightGem != null)
                        {
                            Gem leftGemComponent = leftGem.GetComponent<Gem>();
                            Gem rightGemComponent = rightGem.GetComponent<Gem>();

                            if (leftGemComponent.gemType == currentGemComponent.gemType &&
                                rightGemComponent.gemType == currentGemComponent.gemType)
                            {
                                if (!currentMatches.Contains(leftGem))
                                {
                                    currentMatches.Add(leftGem);
                                }
                                leftGemComponent.isMatched = true;

                                if (!currentMatches.Contains(rightGem))
                                {
                                    currentMatches.Add(rightGem);
                                }
                                rightGemComponent.isMatched = true;

                                if (!currentMatches.Contains(currentGem))
                                {
                                    currentMatches.Add(currentGem);
                                }
                                currentGemComponent.isMatched = true;
                            }
                        }
                    }

                    // Check vertical matches
                    if (y > 0 && y < board.height - 1)
                    {
                        GameObject downGem = board.allGems[x, y - 1];
                        GameObject upGem = board.allGems[x, y + 1];

                        if (downGem != null && upGem != null)
                        {
                            Gem downGemComponent = downGem.GetComponent<Gem>();
                            Gem upGemComponent = upGem.GetComponent<Gem>();

                            if (downGemComponent.gemType == currentGemComponent.gemType &&
                                upGemComponent.gemType == currentGemComponent.gemType)
                            {
                                if (!currentMatches.Contains(downGem))
                                {
                                    currentMatches.Add(downGem);
                                }
                                downGemComponent.isMatched = true;

                                if (!currentMatches.Contains(upGem))
                                {
                                    currentMatches.Add(upGem);
                                }
                                upGemComponent.isMatched = true;

                                if (!currentMatches.Contains(currentGem))
                                {
                                    currentMatches.Add(currentGem);
                                }
                                currentGemComponent.isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void MarkGemsAsUnmatched()
    {
        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                if (board.allGems[x, y] != null)
                {
                    board.allGems[x, y].GetComponent<Gem>().isMatched = false;
                }
            }
        }
    }
}
