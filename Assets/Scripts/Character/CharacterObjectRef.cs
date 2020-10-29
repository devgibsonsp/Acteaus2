using UnityEngine;

namespace CharacterNS
{

    // This static class has become bigger than the original intention of creating a UI lock
    // and now encompasses information about the character in general

    public static class CharacterObject // user interface/ inventory ? lifecycle?
    {

        public static GameObject Ref { get; set; }

        public static bool RefSet { get; set; } = false;

    }
}
