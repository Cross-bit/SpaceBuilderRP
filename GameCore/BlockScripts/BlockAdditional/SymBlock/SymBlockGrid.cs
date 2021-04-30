using UnityEngine;
using Assets.Scripts.GameCore.API.Extensions;

namespace Assets.Scripts.BlockScripts.BlockAdditional
{
    public class SymBlockGrid : IBlockGrid
    {
        private GameObject _gridGameObject;
        private Transform _gridParentTransform;
        private Vector3 _gridPosition;
        private Vector3 _parentBlockPosition;
        private Vector3 _parentBlockRotation;
        private Settings.Checkers_types _parentBlockType;
        private Color _gridColor;

        SymBlock parentBlock;

        // TODO: asi poslat celý objekt bloku
        public SymBlockGrid(SymBlock parentBlock, Transform gridParentGameObject, Vector3 parentBlockTransform, Vector3 gridPosition, Vector3 parentBlockRotation, Settings.Checkers_types parentBlockType)
        {
            this._gridPosition = gridPosition;
            this._parentBlockPosition = parentBlockTransform;
            this._parentBlockRotation = parentBlockRotation;
            this._gridParentTransform = gridParentGameObject;
            this._parentBlockType = parentBlockType;

            this._gridGameObject = TryToGetObjectFromPool();

            this.parentBlock = parentBlock;

            // Barva Gridy
            this._gridColor = Helpers.GetCheckerColorByType(this._parentBlockType);
            this.SetGridColor(new Color(this._gridColor.r, this._gridColor.g, this._gridColor.b, 0.2f));

        }

        // GRAFIKA z POOLU // Grida segmentovaná a následně natahována, aby seděla na rozměry bloku
        private GameObject TryToGetObjectFromPool() =>
            Manager.Instance.objectsToPool.GetFromPool(Settings.PoolTypes.GIZ_GRID_SEGMENT, this._gridPosition, Quaternion.Euler(this._parentBlockRotation), _gridParentTransform);

        /// <summary> ON/OFF grid pod blokem. </summary>
        /// <param name="g_col"></param>
        /// <param name="setTo"></param>
        public void SetGridOrientation()
        {
            SetGridPosition();
            SetGridRotation();
            SetGridScale();
        }

        public void SetGridPosition() => this._gridGameObject.transform.position = this._gridPosition;

        public void SetGridRotation() => this._gridGameObject.transform.rotation = this.parentBlock.BlockContainer.transform.rotation;

        public void SetGridScale() => this._gridGameObject.transform.localScale = parentBlock.BlockDimensions / 10;

        public void SetGridColor(Color g_col)
        {
            this._gridGameObject.transform.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", g_col);
            this._gridGameObject.transform.GetComponent<MeshRenderer>().material.SetColor("_EmissiveColor", g_col);
        }

        public void SetGridActive(bool setTo) => this._gridGameObject.SetActive(setTo);

    }
}

