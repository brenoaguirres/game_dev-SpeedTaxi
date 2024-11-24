namespace SpeedTaxi.Utils
{
    public static class TagManager
    {
        public enum ProjectTags
        {
            Player,
            Ground
        }

        public static string GetTag(ProjectTags tag)
        {
            switch (tag)
            {
                case ProjectTags.Player:
                    return "Player";
                    break;
                case ProjectTags.Ground:
                    return "Ground";
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}