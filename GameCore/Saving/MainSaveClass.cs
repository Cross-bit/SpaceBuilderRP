using System;
using UnityEngine;

class SaveClass
{
    public SaveClass()
    {

    }

    public void SaveGameData() {
        // Uložíme vše co jsme postavily
        SaveBlocks();
    }


    private void SaveBlocks() {

        // Uložíme basic data bloků
        //SaveBlockData saveBlock = new SaveBlockData();
        //foreach (Block b in Settings.blocks) {
       //     ES3.Save<Block>("blocks", b.block);
            //ES3.Save<string>("b_name", b.block_name);
        //}

        // Uložíme zbylé věci

    }

}

