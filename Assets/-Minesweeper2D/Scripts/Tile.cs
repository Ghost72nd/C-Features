﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minesweeper2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        //store x and y coordinate in an array for later use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // Is the current tile a mine?
        public bool isRevealed = false; //  Has the tile already been revealed?
        [Header("Reference")]
        public Sprite[] emptySprites; // List of empty sprites i.e, empty, 1 ,2, 3, etc
        public Sprite[] mineSprites; // The mine sprites

        private SpriteRenderer rend; // Reference to sprite renderer

        void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void Start()
        {
            // Randomly decide if its a mine or not
            isMine = Random.value < .05f;
        }
        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flags the tile as being revealed 
            isRevealed = true;
            // Checks if the tile is a mine
            if (isMine)
            {
                // Sets sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Sets sprite to appropiate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }
    }
}