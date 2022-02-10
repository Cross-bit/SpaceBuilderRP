using UnityEngine;

namespace Assets.Scripts.BlockScripts.BlockAdditional
{
    public class SymetricBlockConstructor : IBlockConstructor
    {
        //public IBlock BlockToConstruct { get; private set; }
        private SymetricBlock _blockToConstruct;
        public IBlock BlockToConstruct { get => _blockToConstruct; set => _blockToConstruct = value as SymetricBlock; }

        //TODO: poslat i BlockSO a udělat čitelnější
        public SymetricBlockConstructor(SymetricBlock blockToConstruct) {
            _blockToConstruct = blockToConstruct;
        }

        // Vytvoří základní objekt bloku ve scéně
        public void CreateBlockContainer () {
            // Generace bloku - pod nový parent 
            _blockToConstruct.BlockContainer = new GameObject(_blockToConstruct.BlockName);
            _blockToConstruct.BlockContainer.transform.parent = World.Instance.gameObject.transform;
        }

        public void CreateBlockGraphicsGameObject()
        {
            // Konečně vytvoříme grafiku objektu
            _blockToConstruct.BlocksMainGraphics = GameObject.Instantiate(_blockToConstruct.BlocksMainGraphics, _blockToConstruct.BlockContainer.transform.position, Quaternion.identity) as GameObject; // TODO: možná přepsat do poolu (manager)
            _blockToConstruct.BlocksMainGraphics.name = _blockToConstruct.BlockName + " - GRAPHICS";
            _blockToConstruct.BlocksMainGraphics.transform.parent = _blockToConstruct.BlockContainer.transform;
            _blockToConstruct.BlocksMainGraphics.gameObject.layer = LayerMask.NameToLayer(Settings.BLOCK_LAYER);
            _blockToConstruct.BlocksMainGraphics.gameObject.SetActive(false);
        }

        public Collider[] FindBlockColliders() => _blockToConstruct.BlocksMainGraphics.gameObject.GetComponents<Collider>();

        public void InitializeBlocksColliders()
        {
            Collider[] cols = FindBlockColliders();

            if (cols.Length > 0)
                _blockToConstruct.BlockColliders.AddRange(cols);
            else
            {
                CreateBlockColliders();
                Debug.LogWarning("Collider bloku musel být vytvořen kódem.");
            }
        }

        // Collidery
        private void CreateBlockColliders()
        {
            // Pokud blok nemá defaultně přiřazený boxCollider, tak ho přidáme.
            BoxCollider newBlockCollider = _blockToConstruct.BlocksMainGraphics.gameObject.AddComponent<BoxCollider>();
            newBlockCollider.size = new Vector3(_blockToConstruct.BlockDimensions.x - 1f, _blockToConstruct.BlockDimensions.y - 1f, _blockToConstruct.BlockDimensions.z - 1f);
            _blockToConstruct.BlockColliders.Add(newBlockCollider);
        }

        public void SetBlockFunctionaly() {
            if (_blockToConstruct.blocksPrimaryFunctionality == Settings.Checkers_types.BLANK)
            {
                if (_blockToConstruct.BaseCheckerNextTo != null)
                    _blockToConstruct.blocksPrimaryFunctionality = _blockToConstruct.BaseCheckerNextTo.checkerType;
            }
        }

        // Vygeneruje checkery objektu
        public void CreateParentTransformForCheckers()
        {
            // Vytvoříme ještě Empty container pro checkery( kvůli organizaci...), jako parent
            _blockToConstruct.CheckersContainer = new GameObject("CHECKERS").transform;
            _blockToConstruct.CheckersContainer.transform.position = _blockToConstruct.BlockContainer.transform.position;
            _blockToConstruct.CheckersContainer.transform.parent = _blockToConstruct.BlockContainer.transform;
        }

        public void CreateBlockCheckers()
        {
            // Vytváříme checkery podle pozice (Z definice bloku)
            short ctr = 0;
            for (int i = 0; i < _blockToConstruct.CheckersPosition.Length; i++)
            {
                // Pokud kontroler, který stavíme nemá nastaveno BLANK
                if (_blockToConstruct.BlockCheckersTypes[i] != Settings.Checkers_types.BLANK)
                { 
                    _blockToConstruct.Checkers.Add(new BlockChecker(_blockToConstruct.CheckersPosition[i], _blockToConstruct.CheckersContainer.transform, _blockToConstruct.BlockName, _blockToConstruct.BlockCheckersTypes[i], true));
                }
                else
                {
                    if (_blockToConstruct.BaseCheckerNextTo != null) {
                        _blockToConstruct.Checkers.Add(new BlockChecker(_blockToConstruct.CheckersPosition[i], _blockToConstruct.CheckersContainer.transform, _blockToConstruct.BlockName,
                        _blockToConstruct.BaseCheckerNextTo.checkerType, true));
                    }
                    else
                    {
                        Debug.LogError("Chybí definice checkru (musí být alespoň BLANK) u objektu: " + _blockToConstruct.BlockName);
                    }
                }
                ctr++;
            }
        }

        public void InitializeBlocksCheckersGraphics()
        {
            SetBlocksCheckersGraphic();
            SetBlocksCheckersMaterial();
        }

        public void CreateBlockGrid() {
            var blockGrid = new SymBlockGrid(_blockToConstruct, _blockToConstruct.BlockContainer.transform, _blockToConstruct.BlockDimensions, _blockToConstruct.BlockContainer.transform.position, _blockToConstruct.BlockRotation, _blockToConstruct.blocksPrimaryFunctionality);

            _blockToConstruct.BlockGrid = blockGrid;
            blockGrid.SetGridOrientation(); // todo nefunguje
        }

        // MonoBehaviour – Controller
        public void CreateBlockController()
        {
            // KONTROLÉR (CS)
            var dims = _blockToConstruct.BlockDimensions;
            _blockToConstruct.blockController = _blockToConstruct.BlocksMainGraphics.AddComponent<BlockController>();
            _blockToConstruct.blockController.colDetectionBoxes = dims;
        }

        private void SetBlocksCheckersGraphic()
        {
            for (int i = 0; i < _blockToConstruct.Checkers.Count; i++)
            {
                _blockToConstruct.Checkers[i].SetCheckerGraphics(_blockToConstruct.BlocksMainGraphics);
            }
        }

        private void SetBlocksCheckersMaterial() {
            for (int i = 0; i < _blockToConstruct.Checkers.Count; i++) {
                _blockToConstruct.Checkers[i].SetCheckerMaterial();
            }
        }

    }


}
