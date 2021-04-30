using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GameCore.API.Extensions;
using UnityEngine;

namespace Assets.Scripts.GameCore.BlockScripts.BlockAdditional
{

     // např. jestli blok nezasahuje do jiného apod. 
    public class BlockPlacementValidator {
        private SymBlock _block;
        private Vector3 _currentBlockRotation;
        public bool isPlacementValid;

        public BlockPlacementValidator(SymBlock block, Vector3 currentBlockRotation)
        {
            _block = block;
            _currentBlockRotation = currentBlockRotation;
        }

        public bool CheckIfBlocksPlacementIsValid()
        {

            // Defultní AABB box pro THIS. block
            var block_bounds_dims = new Vector3(_block.BlockDimensions.x, _block.BlockDimensions.y, _block.BlockDimensions.z);
            var block_bounds_dims_rotated = block_bounds_dims.RotateOnY(_currentBlockRotation.y);

            Bounds block_bounds = new Bounds(_block.BlockContainer.transform.position, block_bounds_dims_rotated); // Použijeme AABB(axis-aligned bounding box) box pro detekci overlappingu
            block_bounds.size = block_bounds.size.ABS();

            //(DEV GIZMOS) žluté boxy
            if(_block.blockController != null)
                _block.blockController.colDetectionBoxes = block_bounds_dims_rotated; // DEBUG:

            // Definice bodů pro 
            Vector3[] def_points = new Vector3[4];
            Vector3 b_pos = _block.BlockContainer.transform.position;
            Vector3 b_dims = _block.BlockDimensions.AddScalarToVector(-0.1f).RotateOnY(_currentBlockRotation.y);

            def_points[0] = new Vector3(b_pos.x + b_dims.x / 2, _block.BlockContainer.transform.position.y, b_pos.z + b_dims.z / 2);
            def_points[1] = new Vector3(b_pos.x + b_dims.x / 2, _block.BlockContainer.transform.position.y, b_pos.z - b_dims.z / 2);
            def_points[2] = new Vector3(b_pos.x - b_dims.x / 2, _block.BlockContainer.transform.position.y, b_pos.z + b_dims.z / 2);
            def_points[3] = new Vector3(b_pos.x - b_dims.x / 2, _block.BlockContainer.transform.position.y, b_pos.z - b_dims.z / 2);

            // Nejdřív ověříme zda veškeré kontroléry jsou empty, nebo pokud nejsou zda typy sedí

            // Získáme veškeré bloky v blízkosti 
            float searchRadius = _block.symConstant * 2f + Settings.largestSymConstant + Mathf.Sqrt(2 * Mathf.Pow(Settings.largestSymConstant, 2)) + 1f; // + 1f Pro případ v rozých čtverce(opsanýpoloměr čtverci) => pythagoras(square root) ...
            List<SymBlock> all_blocks_near = Helpers.GetAllBlocksInRadius(_block, searchRadius);

            int[] common_checkers_ctr = new int[_block.Checkers.Count];
            foreach (SymBlock b_around in all_blocks_near) // TODO: Mohlo by být efektivnější s využitím znalosti rotace(tzn. get Objet rotation)
            {

                // AABB bloku 
                Vector3 b_around_bounds_dims = b_around.BlockDimensions.AddScalarToVector(-0.1f).RotateOnY(b_around.BlockRotation.y);
                Bounds b_around_bounds = new Bounds(b_around.BlockContainer.transform.position, b_around_bounds_dims);
                //b_around_bounds.

               // Debug.Log(b_around.BlockName);
                b_around_bounds.size = b_around_bounds.size.ABS();
                foreach (Vector3 def_point in def_points)
                {
                    if (b_around_bounds.Contains(def_point))
                    {
                        Debug.Log("bod je v definici" + def_point);
                        return false;
                    }
                }

                if (block_bounds.Intersects(b_around_bounds))
                {
                    Debug.Log("Přes classu to jde: " + b_around.BlockContainer.name);
                    return false;
                }

                foreach (BlockChecker b_around_c in b_around.Checkers)
                {

                    int ctr = 0;
                    foreach (BlockChecker c in _block.Checkers)
                    {
                        if (c.CheckerTransform.transform.position != _block.BaseCheckerNextTo.CheckerTransform.transform.position)
                        {
                            if (b_around_c.CheckerTransform.transform.position == c.CheckerTransform.transform.position)
                            {
                                // 1. KONTROLA - KONTROLUJE SE, ZDA KONTROLÉR BLOKU NENARAZÍ NA JINÝ TYP, NEŽ TAKOVÝ, JAKÝM JE DEFINOVÁN.
                                if (b_around_c.checkerType != c.checkerType)
                                {
                                    //st.Stop();
                                    /*Debug.Log(string.Format("MyMethod took {0} ms to complete", st.ElapsedMilliseconds));*/
                                    Debug.Log("Selhala 1.");
                                    return false; // Rovnou vrátíme
                                }

                                // 2. KONTROLA
                                common_checkers_ctr[ctr]++;

                                if (ctr == 0)
                                {
                                    /*  Debug.Log(b_around.block.transform.position);
                                      Debug.Log(b_around_c.checker_game_obj.transform.position);
                                      Debug.Log(c.checker_game_obj.transform.position);*/
                                }
                            }
                        }

                        ctr++;
                    }
                    #region Dev Notes
                    // Pro tuto kontrolu je nezbytné vypnout veškeré kontroléry v okruhu
                    // Pozn. Teď proč to dělám... Je to proto, že v dalším kroku kontroly je zapotřebí zkontrolovat, zda se objekt náhodou nekryje s objektem jiným.
                    // Byly zkoušeny metody:
                    // 1. if, kde porovnávám pozice jednotlivých stran bloků zda se nekryjí => nepovedlo se
                    // 2. BlockController => onTrigger X onCollider => nepovedlo se, jelikož alespoň jeden z objektů musí obsahovat rigidbody.
                    // 3. bounds intersection => nefungje dle představ !!!! Pozor na zápornou velikost!!! Protože si to neumí ošetřit!!!!!!!!!!!!!!!!!!!!! 
                    // 4. Raycast napříč celým bloke => funkguje, ale z neznámého důvodu nedokáže nalézt Block layer, 
                    //i přesto, že je na správné pozici, je správný, ale prostě nefunguje => provizorní řešení => vypneme veškeré collidery a následně provádmí kontrolu na jakoukoliv intersekci.
                    #endregion
                }
            }

            // 2. KONTROLA - Pokud kontrolér narazí na více jak 1 další kontrolér
            for (int i = 0; i < common_checkers_ctr.Length; i++) {
                if (common_checkers_ctr[i] > 1) {
                    //Debug.Log("Selhala 2.");
                    return false;
                }
            }

            return true;//;isOrientationValid;

        }


    }

}
