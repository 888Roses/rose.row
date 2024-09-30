using UnityEngine;
using UnityEngine.UI;

namespace rose.row.easy_package.ui.factory.elements
{
    public class GridElement : AbstractLayoutGroupElement
    {
        private GridLayoutGroup _grid;
        public GridLayoutGroup grid => _grid;

        protected override void Awake()
        {
        }

        public override void build()
        {
            _grid = use<GridLayoutGroup>();
        }

        public void setTileSize(Vector2 tileSize) => _grid.cellSize = tileSize;

        public void setTileSize(float width, float height) => setTileSize(new Vector2(width, height));

        public void setTileSize(float size) => setTileSize(size, size);

        public void setSpacing(Vector2 spacing)
            => _grid.spacing = spacing;

        public void setSpacing(float spacingX, float spacingY)
            => setSpacing(new Vector2(spacingX, spacingY));

        public void setSpacing(float spacing)
            => setSpacing(spacing, spacing);

        protected override LayoutGroup layoutGroup => _grid;
    }
}