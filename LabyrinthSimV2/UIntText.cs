namespace LabyrinthSim
{
    class UIntText : ValText<uint>
    {
        public UIntText() : this(0u)
        {
        }

        public UIntText(uint value)
        {
            Value = value;
            Text = Convert(value);
        }

        protected override uint Convert(string text)
        {
            if (text.Length == 0) return 0u;

            return uint.TryParse(text, out uint value) ? value : Value;
        }

        protected override string Convert(uint value)
        {
            return value.ToString();
        }
    }
}
