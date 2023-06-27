using System.Collections;
using UnityEngine.SceneManagement;

namespace Camoak.Tests.Dsl.Game
{
    public class GameTestSetup
    {
        public const IEnumerable WAIT_SINGLE_FRAME = null;

        public static IEnumerable SetScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            return WAIT_SINGLE_FRAME;
        }
    }
}
