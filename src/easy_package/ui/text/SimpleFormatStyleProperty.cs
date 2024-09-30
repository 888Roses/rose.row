namespace rose.row.easy_package.ui.text
{
    public class SimpleFormatStyleProperty : AbstractStyleProperty
    {
        private bool _hasTag;
        public bool hasTag => _hasTag;

        private string _tag;
        public string tag => _tag;

        public void set(bool value)
        {
            _hasTag = value;
        }

        public SimpleFormatStyleProperty(string tag, bool hasTag = true)
        {
            _tag = tag;
            _hasTag = hasTag;
        }

        public SimpleFormatStyleProperty()
        {
            _hasTag = false;
        }

        public override string getString(string text)
        {
            if (_hasTag)
                return $"<{tag}>{text}</{tag}>";
            else
                return text;
        }
    }
}