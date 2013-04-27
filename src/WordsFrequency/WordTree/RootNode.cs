namespace WordsFrequency.WordTree
{
    public class RootNode : TreeNode
    {
        protected override LetterNode CreateChild(char symbol)
        {
            return new LetterNode(symbol);
        }
    }
}