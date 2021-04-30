using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveLoadData
{
    public List<SymBlock> blocks;
    public List<string> test_st = new List<string>(6); 

   // public int health;



    public SaveLoadData(List<SymBlock> _block)
    {
        blocks = _block;
    }

}
