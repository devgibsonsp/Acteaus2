namespace UI
{

    // This static class has become bigger than the original intention of creating a UI lock
    // and now encompasses information about the character in general

    public static class UserInterfaceLock // user interface/ inventory ? lifecycle?
    {
        public static bool IsLocked { get; set; } = false;

        public static bool IsDragging { get; set; } = false;

        public static ItemProperties DraggedItem { get; set; }

        public static CharacterStatistics CharacterReference { get; set; }

    }
}
