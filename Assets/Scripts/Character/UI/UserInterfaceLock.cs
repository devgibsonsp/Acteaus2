namespace UI
{
    public static class UserInterfaceLock // user interface/ inventory ? lifecycle?
    {
        public static bool IsLocked { get; set; } = false;

        public static bool IsDragging { get; set; } = false;

        public static ItemProperties DraggedItem { get; set; }
    }
}
