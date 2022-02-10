using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.WorldBuilding.BlockLibrary
{
    public static class BlockLibrary
    {
        /// <summary>Databáze bloků</summary> 
         public static List<IBlock> blocksLib = new List<IBlock>();

        /// <summary>
        /// Přidání bloku do databáze.
        /// </summary>
        /// <param name="blockToAdd"></param>
        public static void AddBlockToBlocksLib(IBlock blockToAdd) => blocksLib.Add(blockToAdd);

        /// <summary>
        /// Odstranění bloku z databáze.
        /// </summary>
        /// <param name="blockToAdd"></param>
        public static void RemoveBlockFromTheDatabase(IBlock blockToRemove) => blocksLib.Remove(blockToRemove);

    }
}
